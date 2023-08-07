using MaterialDesignThemes.Wpf.Transitions;
using RoomManagement.UserControls;
using RoomManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RemoteInerfaces.Services.AdminServices;
using RemoteInerfaces.Entities;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.IO;
using System.Drawing;
using RemoteInerfaces.Services.DoorAppServices;
using RemoteInerfaces.Services;
using System.Windows.Forms;
using Emgu.CV.UI;
using Emgu.CV;
using System.Runtime.InteropServices;
using RemoteInerfaces;

namespace RoomManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LanguageItems model { get; set; }
        static IAdminRoomService adminRoomService;
        public static IAdminDeviceService adminDeviceService = (IAdminDeviceService) Activator.GetObject(typeof(IAdminDeviceService), "tcp://localhost:8080/AdminDeviceService");
        IAccountService accountService = (IAccountService) Activator.GetObject(typeof(IAccountService), "tcp://localhost:8080/AccountService");
        IActionsHistory actionsHistoryService = (IActionsHistory) Activator.GetObject(typeof(IActionsHistory), "tcp://localhost:8080/ActionsHistoryService");
        IDoorRequestService doorRequestService = (IDoorRequestService)Activator.GetObject(typeof(IDoorRequestService), "tcp://localhost:8080/DoorRequestService");
        IHouseService houseService = (IHouseService)Activator.GetObject(typeof(IHouseService), "tcp://localhost:8080/HouseService");
        Temperatur_HumidityInterface temeprature_humidity = (Temperatur_HumidityInterface)Activator.GetObject(typeof(Temperatur_HumidityInterface), "tcp://localhost:8081/Temperature_Humidity");
        SlideWipe SlideWipeLeft = new SlideWipe() { Direction = SlideDirection.Left, Duration = TimeSpan.FromSeconds(0.5) };
        SlideWipe SlideWipeRight = new SlideWipe() { Direction = SlideDirection.Right, Duration = TimeSpan.FromSeconds(0.5) };
        static Account account = new Account();
        DispatcherTimer refreshNotification;
        DispatcherTimer littTimer;
        DispatcherTimer littTimer2;
        DispatcherTimer liveUpdate;
        Capture capture;
        int nonActiveAccountCounter = 0;
        ObservableCollection<ActionHistory> deviceActionsHistoriesForGrid = new ObservableCollection<ActionHistory>();
        ObservableCollection<Account> accoountsForGrid = new ObservableCollection<Account>();
        public ObservableCollection<ActiveDevice> activeDevicesForGrid { get; set; }
        public string HouseKey { get; set; }
        public List<RoomDevice> roomDevices { get; set; }
        public ObservableCollection<UserControls.Slider> Rooms { get; set; }
        public List<RoomWithDevieces> rooms;
        public ObservableCollection<ComboBoxItem> ComboBoxRoomsList { get; set; }
        public MainWindow()
        {
            int account_id = 1;
            account = accountService.getAccount(account_id);
            House house = houseService.getHouse(account.id_house);
            
            adminRoomService = (IAdminRoomService)Activator.GetObject(typeof(IAdminRoomService), "tcp://localhost:8080/AdminRoomService");
            //adminDeviceService = 
            initializeRoomSliders();
            model = new LanguageItems();

            List<Account> accooountsAsList = accountService.getHouseAccount(account.id_house);
            foreach(Account account in accooountsAsList)
            {
                if(account.Id == MainWindow.account.Id)
                {
                    continue;
                }


                if (!account.is_Active)
                {
                    nonActiveAccountCounter += 1; 
                }
                accoountsForGrid.Add(account);
            }

            List<DeviceActionsHistory> deviceActionsHistories = actionsHistoryService.getHouseLastActions(account.Id);
            foreach(DeviceActionsHistory deviceAction in deviceActionsHistories)
            {
                Account executer_account = accountService.getAccount(deviceAction.id_executer);
                Device deviceOfAction = adminDeviceService.getDevice(deviceAction.id_device);
                ActionHistory actionHistory = new ActionHistory(executer_account.profile_image, executer_account.First_Name, deviceOfAction.type, deviceAction.action_type, deviceAction.date_time.ToString());
                deviceActionsHistoriesForGrid.Add(actionHistory);
            }
            
            activeDevicesForGrid = new ObservableCollection<ActiveDevice>();
            Dictionary<Room, Device> activeDevicesWithRoom = adminDeviceService.getActiveHouseDevices(account.id_house);
            foreach(KeyValuePair<Room, Device> entry in activeDevicesWithRoom)
            {
                ActiveDevice activeDevice = new ActiveDevice() { activeDeviceImage = $"/Images/{entry.Value.type}_off.png", activeDeviceRoom = entry.Key.descriptive_name, activeDeviceType = entry.Value.type };
                activeDevicesForGrid.Add(activeDevice);
                if (activeDevicesForGrid.Count() == 4)
                {
                    break;
                }
            }


           

            InitializeComponent();
            HouseKeyTextBlock.Text = house.house_key;
            welcomingTitleToAdmin.Text = "Welcome Mr. "+ account.First_Name;
            //DataContext = this;
            Loaded += MainWindow_Loaded;
            RoomCombobox.SelectedIndex = 0;
            //if (Rooms.Count() == 0)
            //{
            //    noRoomAvailable.Visibility = Visibility.Visible;
            //    RoomsAvailable.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    noRoomAvailable.Visibility = Visibility.Collapsed;
            //    RoomsAvailable.Visibility = Visibility.Visible;
            //}
            refreshNotification = new DispatcherTimer();
            refreshNotification.Interval = TimeSpan.FromSeconds(5);
            refreshNotification.Tick += refreshNotificationPanel;
            refreshNotification.Start();
            membersDataGrid.ItemsSource = deviceActionsHistoriesForGrid;
            AccountsDataGrid.ItemsSource = accoountsForGrid;
            nonActiveAccountTextBox.Text = nonActiveAccountCounter != 0 ? (nonActiveAccountCounter != 1 ? $"{nonActiveAccountCounter} Non Active Accounts" : $"{nonActiveAccountCounter} Non Active Account") : "All Accounts Active";
            dashboardWindow.Visibility = Visibility.Visible;
        }


        public void live_tick(object sender, EventArgs e) {
            if(capture.QueryFrame() != null)
            {
                cameraLive.Source = ToBitmapSource(capture.QueryFrame());
            }
        }

      

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap  

                BitmapSource bs = System.Windows.Interop
                  .Imaging.CreateBitmapSourceFromHBitmap(
                  ptr,
                  IntPtr.Zero,
                  Int32Rect.Empty,
                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap  
                return bs;
            }
        }

        public static void deviceStateChanged(int id_device, bool state)
        {
            adminDeviceService.changeDeviceState(account.id_house, account.Id, id_device, state);
        }

        private void refreshNotificationPanel(object sender, EventArgs e)
        {
            DoorRequest request = doorRequestService.getLastRequest();
            if (request != null)
            {
                notificationTextBlock.Text = "Mr. " + request.full_name + " wants to open the door";
                if (!request.request_message.Trim().Equals(""))
                {
                    requestResponseMessage.Text = request.request_message;
                }



                notificationUserImage.Source = GetImage(request.face_image);
                doorRequestNotification.Visibility = Visibility.Visible;
                littTimer = new DispatcherTimer();
                littTimer.Interval = TimeSpan.FromMilliseconds(500);
                littTimer.Tick += waitOneSec;
                littTimer.Start();
            }

        }

        private void waitOneSec(object sender, EventArgs e)
        {
            littTimer.Stop();
            doorRequestNotification.SelectedIndex = 1;
        }

        private void waitOneSec2(object sender, EventArgs e)
        {
            littTimer2.Stop();
            doorRequestNotification.Visibility = Visibility.Collapsed;

        }

        public static BitmapImage GetImage(byte[] image)
        {
            try
            {

                Bitmap bitmap;
                using (var stream = new MemoryStream(image))
                {
                    bitmap = new Bitmap(stream);
                }
                Console.WriteLine("is it null?");
                return Convert(bitmap);
            }
            catch
            {
                return null;
            }
        }


        private static BitmapImage Convert(object value)
        {
            Console.WriteLine("is it null? 2");
            if (value != null && value is System.Drawing.Image image)
            {
                Console.WriteLine("it is not");
                var memoryStream = new MemoryStream();
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                bitmap.StreamSource = memoryStream;
                bitmap.EndInit();

                bitmap.Freeze();

                return bitmap;
            }

            return null;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            capture = new Capture();
            liveUpdate = new DispatcherTimer();
            liveUpdate.Interval = new TimeSpan(0, 0, 0, 0, 1);
            liveUpdate.Tick += new EventHandler(live_tick);
            liveUpdate.Start();

        }

        private void initializeRoomSliders()
        {
             
            rooms = adminRoomService.getAdminRooms(account.Id);
            Rooms = new ObservableCollection<UserControls.Slider>();
            ComboBoxRoomsList = new ObservableCollection<ComboBoxItem>();

            foreach (RoomWithDevieces room in rooms)
            {
                UserControls.Slider slider = new UserControls.Slider();
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                ObservableCollection<RoomDevice> roomDevices = new ObservableCollection<RoomDevice>();
                int deviceIndex = 0;
                foreach(Device device in room.room_devices)
                {
                    RoomDevice roomDevice = new RoomDevice();
                    roomDevice.Title = device.descriptive_name;
                    roomDevice.Device_ID = device.id_device;
                    roomDevice.ColumnGrid = deviceIndex % 4;
                    roomDevice.RowGrid = deviceIndex < 4 ? 0 : 1;
                    roomDevice.ImageOffSource = $"/Images/{device.type}_off.png";
                    roomDevice.ImageOnSource = $"/Images/{device.type}_on.png";
                    roomDevice.is_active = device.state;
                    deviceIndex++;
                    roomDevices.Add(roomDevice);
                }
                slider.Title = room.room.descriptive_name;
                slider.ImageSourceAsBytes = room.room.background_image;
                //roomDevices.Add(new RoomDevice() { Title = "Temperature And Humidity", ColumnGrid = 0, RowGrid = 0, ImageOffSource = "/Images/Temperature_Humidity_off.png", ImageOnSource= "/Images/Temperature_Humidity_on.png" });
                //roomDevices.Add(new RoomDevice() { Title = "Camera", ColumnGrid = 1, RowGrid = 0, ImageOffSource = "/Images/Camera_off.png", ImageOnSource = "/Images/Camera_on.png" });
                //roomDevices.Add(new RoomDevice() { Title = "TV", ColumnGrid = 2, RowGrid = 0, ImageOffSource = "/Images/TV_off.png", ImageOnSource = "/Images/TV_on.png" });

                slider.RoomDevicesList = roomDevices;
                slider.BackwardWipe = SlideWipeLeft;
                slider.ForwardWipe = SlideWipeRight;
                slider.RoomID = "Room"+room.room.id_room.ToString();
                slider.Temperature = temeprature_humidity.getTemperature();
                slider.Humidity = temeprature_humidity.getHumidity();

                comboBoxItem.Content = room.room.descriptive_name;
                comboBoxItem.Uid = Rooms.Count().ToString();
                comboBoxItem.Padding = new Thickness(20);
                Rooms.Add(slider);
                ComboBoxRoomsList.Add(comboBoxItem);
            }

            
        }
        
        private void reInitializeMultiSelectomboBox()
        {
            var GridChild = HelperClass.GetChildOfType<Grid>(CustomMultiSelectComboBox);
            Console.WriteLine("started");
            if (GridChild == null) return;
            Console.WriteLine("grid not null");
            var toggleButtonChild = HelperClass.GetChildOfType<ToggleButton>(GridChild);
            if (toggleButtonChild == null) return;
            Console.WriteLine("toggleButton not null");
            var contentControlChild = HelperClass.GetChildOfType<ContentControl>(toggleButtonChild);
            if (contentControlChild == null) return;
            Console.WriteLine("contentControl not null");
            var contentPresenterlChild = HelperClass.GetChildOfType<ContentPresenter>(contentControlChild);
            if (contentPresenterlChild == null) return;
            Console.WriteLine("contentPresenter not null");
            var borderChild = HelperClass.GetChildOfType<Border>(contentPresenterlChild);
            if (borderChild == null) return;
            Console.WriteLine("border not null");

            borderChild.BorderBrush = (System.Windows.Media.Brush) new BrushConverter().ConvertFrom("#E1E6EB");
            borderChild.Background = System.Windows.Media.Brushes.Transparent;
            borderChild.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borderChild.BorderThickness = new Thickness(1);
            Console.WriteLine("all is good");

        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox textBox && textBox.LineCount > 0)
            {
                textBox.ScrollToLine(textBox.LineCount - 1);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;
        private byte[] roomBackgroundImage;
        private string fullPathImage;

        public void ActivateAccount_click(object sender, RoutedEventArgs e)
        {
            var accountToActivate = e.Source as FrameworkElement;
            if(accountToActivate != null)
            {
                int accountId = System.Convert.ToInt32(accountToActivate.Uid);
                accountService.activateAccount(accountId);
                accoountsForGrid.Where(account => account.Id == accountId).FirstOrDefault().is_Active = true;
                AccountsDataGrid.ItemsSource = accoountsForGrid;
            }
        }

        public void disActivateAccount_click(object sender, RoutedEventArgs e)
        {
            var accountToDisActivate = e.Source as FrameworkElement;
            if (accountToDisActivate != null)
            {
                int accountId = System.Convert.ToInt32(accountToDisActivate.Uid);
                accountService.disactivateAccount(accountId);
                foreach(Account account1 in accoountsForGrid)
                {
                    if(accountId == account1.Id)
                    {
                        account1.is_Active = false;
                    }


                }
                AccountsDataGrid.ItemsSource = accoountsForGrid;
            }
        }

        public void deleteAccount_click(object sender, RoutedEventArgs e)
        {
            RoomsAvailable.Visibility = Visibility.Visible;
            dashboardWindow.Visibility = Visibility.Collapsed;
        }

        

        public void activateRoomManagementWindow_click(object sender, RoutedEventArgs e)
        {
            if (Rooms.Count() == 0)
            {
                noRoomAvailable.Visibility = Visibility.Visible;
                RoomsAvailable.Visibility = Visibility.Collapsed;
            }
            else
            {
                noRoomAvailable.Visibility = Visibility.Collapsed;
                RoomsAvailable.Visibility = Visibility.Visible;
            }
            HouseKeyWindow.Visibility = Visibility.Collapsed;
            dashboardWindow.Visibility = Visibility.Collapsed;
            AccountManagementWindow.Visibility = Visibility.Collapsed;
        }

        public void activateDashboardWindow_click(object sender, RoutedEventArgs e) {
            dashboardWindow.Visibility = Visibility.Visible;    
            noRoomAvailable.Visibility = Visibility.Collapsed;
            HouseKeyWindow.Visibility = Visibility.Collapsed;
            RoomsAvailable.Visibility = Visibility.Collapsed;
            AccountManagementWindow.Visibility = Visibility.Collapsed;
        }
        
        public void activateAccountManagementWindow_click(object sender, RoutedEventArgs e)
        {
            AccountManagementWindow.Visibility = Visibility.Visible;
            RoomsAvailable.Visibility = Visibility.Collapsed;
            HouseKeyWindow.Visibility = Visibility.Collapsed;
            noRoomAvailable.Visibility = Visibility.Collapsed;
            dashboardWindow.Visibility = Visibility.Collapsed;
        }
        public void stopCamera_click(object sender, RoutedEventArgs e) {
            liveUpdate.Stop();
            stopCameraButton.Visibility = Visibility.Collapsed;
            restartCameraButton.Visibility = Visibility.Visible;
            capture.Dispose();
        }

        public void restartCamera_click(object sender, RoutedEventArgs e)
        {
            capture = new Capture();
            liveUpdate.Start();
            stopCameraButton.Visibility = Visibility.Visible;
            restartCameraButton.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (myTransitioner.SelectedIndex < Rooms.Count - 1)
                myTransitioner.SelectedIndex += 1;
            //page1.Visibility = Visibility.Hidden;
            RoomCombobox.SelectedValue = myTransitioner.SelectedIndex.ToString();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (myTransitioner.SelectedIndex > 0)
                myTransitioner.SelectedIndex -= 1;
            RoomCombobox.SelectedValue = myTransitioner.SelectedIndex.ToString();
        }

        public void newDeviceFormButton_click(object sender, RoutedEventArgs e)
        {
            newDeviceForm.Visibility = Visibility.Visible;
        }


        private void RoomCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)RoomCombobox.SelectedItem;
            int value = System.Convert.ToInt32(typeItem.Uid.ToString());
            if (value != myTransitioner.SelectedIndex)
                myTransitioner.SelectedIndex = value;

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null) return;
            var tb = HelperClass.GetChildOfType<DatePickerTextBox>(dp);
            if (tb == null) return;
            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;
            wm.Content = "Select Your BirthDate";
        }

        private void chooseProfileImageButtonClick(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Please select your profile image";
            bool? responce = openFileDialog.ShowDialog();
            if (responce == true)
            {
                var fullPath = openFileDialog.FileName;
                profileImagePath.Text = fullPath;
                ImageBrush image = new ImageBrush();
                var fileSource = System.IO.Path.Combine(fullPath);
                BitmapImage bitmapImage = new BitmapImage(new Uri(fileSource));

                roomBackgroundImage = System.IO.File.ReadAllBytes(fullPath);


                image.ImageSource = bitmapImage;
                //ProfileImage.Fill = image;
                fullPathImage = fullPath;
            }
        }

        private void MultiSelectComboBox_Loaded(object sender, RoutedEventArgs e)
        {

            reInitializeMultiSelectomboBox();
        }

        private void AddNewRoom_Click(object sender, RoutedEventArgs e)
        {
            newRoomForm.Visibility = Visibility.Visible;
        }
        
        private void newRoomCancel_Click(object sender, RoutedEventArgs e)
        {
            newRoomForm.Visibility = Visibility.Collapsed;
        }
        
        private void newRoomSave_Click(object sender, RoutedEventArgs e)
        {
            //Add to db
            Room newRoom = new Room();
            newRoom.id_house = 1;
            newRoom.descriptive_name = descriptiveNameTextBox.Text;
            newRoom.background_image = roomBackgroundImage;
            ObservableCollection<LanguageItem> items = (ObservableCollection<LanguageItem>)CustomMultiSelectComboBox.SelectedItems;
            List<RoomAccessors> roomAccessors = new List<RoomAccessors>();
            foreach(LanguageItem item in items)
            {
                RoomAccessors roomAccessor = new RoomAccessors();
                roomAccessor.id_account = item.Id;
                roomAccessors.Add(roomAccessor);
            }
            RoomWithDevieces newRoomWithDevieces = new RoomWithDevieces();
            newRoomWithDevieces.room = newRoom;
            rooms.Add(newRoomWithDevieces);
            adminRoomService.createNewRoom(newRoom, roomAccessors);


            Rooms.Add(
                new UserControls.Slider() { 
                    Title = descriptiveNameTextBox.Text,
                    ImageSourceAsBytes = roomBackgroundImage,
                    RoomDevicesList = new ObservableCollection<RoomDevice>(),
                    BackwardWipe = SlideWipeLeft,
                    ForwardWipe = SlideWipeRight 
                }
            );
            ComboBoxRoomsList.Add( 
                new ComboBoxItem() { 
                    Content = descriptiveNameTextBox.Text,
                    Uid = ComboBoxRoomsList.Count().ToString(),
                    Padding = new Thickness(20)
                }
            );
            newRoomForm.Visibility = Visibility.Collapsed;
            noRoomAvailable.Visibility = Visibility.Collapsed;
            RoomsAvailable.Visibility = Visibility.Visible;

        }

        private void newDeviceSave_Click(object sender, RoutedEventArgs e)
        {
            if (rooms[myTransitioner.SelectedIndex].room_devices.Count < 8)
            {
                RoomWithDevieces targetedRoom = rooms[myTransitioner.SelectedIndex];
                Device newDevice = new Device();
                newDevice.descriptive_name = descriptiveDeviceNameTextBox.Text;
                newDevice.id_room = targetedRoom.room.id_room;
                ComboBoxItem comboBoxItem = (ComboBoxItem) deviceTypeComboBox.SelectedItem;
                Console.WriteLine(comboBoxItem.Content.ToString());
                newDevice.type = comboBoxItem.Content.ToString();
                newDevice.url = DeviceUrlTextBox.Text;
                rooms[myTransitioner.SelectedIndex].room_devices.Add(newDevice);
                adminDeviceService.addNewDevice(newDevice);
                RoomDevice newRoomDevice = new RoomDevice();
                newRoomDevice.ColumnGrid = targetedRoom.room_devices.Count() % 4;
                newRoomDevice.RowGrid = targetedRoom.room_devices.Count() < 4 ? 0 : 1;
                newRoomDevice.Title = newDevice.descriptive_name;
                newRoomDevice.ImageOffSource = $"/Images/{newDevice.type}_off.png";
                newRoomDevice.ImageOnSource = $"/Images/{newDevice.type}_on.png";
                Rooms[myTransitioner.SelectedIndex].RoomDevicesList.Add(newRoomDevice);
            }
            newDeviceForm.Visibility = Visibility.Collapsed;

        }

        private void newDeviceCancel_Click(object sender, RoutedEventArgs e)
        {
            newDeviceForm.Visibility = Visibility.Collapsed;
        }

        private void AcceptRequest_click(object sender, RoutedEventArgs e)
        {
            doorRequestService.respondToDoorRequest(account.Id, true, responseMessageTextBox.Text);
            doorRequestNotification.SelectedIndex = 0;
            littTimer2 = new DispatcherTimer();
            littTimer2.Interval = TimeSpan.FromMilliseconds(500);
            littTimer2.Tick += waitOneSec2;
            littTimer2.Start();
        }

        private void DenyRequest_click(object sender, RoutedEventArgs e)
        {
            doorRequestService.respondToDoorRequest(account.Id, false, responseMessageTextBox.Text);
            doorRequestNotification.SelectedIndex = 0;
            littTimer2 = new DispatcherTimer();
            littTimer2.Interval = TimeSpan.FromMilliseconds(500);
            littTimer2.Tick += waitOneSec2;
            littTimer2.Start();
        }

        private void ActivateHouseKeyWindow_click(object sender, RoutedEventArgs e)
        {
            AccountManagementWindow.Visibility = Visibility.Collapsed;
            RoomsAvailable.Visibility = Visibility.Collapsed;
            HouseKeyWindow.Visibility = Visibility.Visible;
            noRoomAvailable.Visibility = Visibility.Collapsed;
            dashboardWindow.Visibility = Visibility.Collapsed;
        }
    }

    public class ComboBoxRoomItem
    {
        public string Uid { get; set; }
        public string RoomTitle { get; set; }
    }

    public class ActionHistory
    {
        public byte[] AccountImageSource { get; set; }
        public string Name { get; set; }
        public string DeviceType { get; set; }
        public string ActionType { get; set; }
        public string ActionTime { get; set; }

        public ActionHistory(byte[] accountImage, string name, string device_type, string action_type, string action_time) {
            AccountImageSource = accountImage;
            Name = name;
            DeviceType = device_type;
            ActionType = action_type;
            ActionTime = action_time;
        }
    }

    public class ActiveDevice
    {
        public string activeDeviceType { get; set; }
        public string activeDeviceRoom { get; set; }
        public string activeDeviceImage { get; set; }
    }

    ////public class RoomDevice 
    //{
    //    public string Title { get; set; }
    //    public string ImageOffSource { get; set; }
    //    public string ImageOnSource { get; set; }
    //    public int ColumnGrid { get; set; }
    //    public int RowGrid { get; set; }


    //}
}
