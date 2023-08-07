using Emgu.CV;
using Emgu.CV.Structure;
using RemoteInerfaces.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace SignIn_Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Capture camera;
        HaarCascade faceDetected;
        Image<Bgr, Byte> frame;
        DispatcherTimer timer2;
        Image<Gray, byte> result;
        Image<Gray, byte> grayFace = null;
        Account account = null;
        House house = null;
        byte[] accountFaceID = null;
        byte[] accountProfileImage = null;
        RemoteInerfaces.Services.IAccountService accountService;

        public MainWindow()
        {
            accountService = (RemoteInerfaces.Services.IAccountService)Activator.GetObject(typeof(RemoteInerfaces.Services.IAccountService), "tcp://localhost:8080/AccountService");
            faceDetected = new HaarCascade("haarcascade_frontalface_default.xml");
            InitializeComponent();
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

        private void start_faceDetection(object sender, MouseButtonEventArgs e)
        {
            LoadingIcon.Visibility = Visibility.Visible;
            ErrorLabel.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Hidden;
            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(10);
            timer2.Tick += faceDetectionResult;
            timer2.Start();
            camera = new Capture();
            camera.QueryFrame();
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new Action(FrameProcedureForGuest));

        }

        private void FrameProcedureForGuest()
        {
            try
            {
                if (camera.QueryFrame() != null)
                {
                    frame = camera.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    grayFace = frame.Convert<Gray, Byte>();
                    MCvAvgComp[][] facesDetectedNow = grayFace.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new System.Drawing.Size(20, 20));
                    foreach (MCvAvgComp f in facesDetectedNow[0])
                    {
                        result = frame.Copy(f.rect).Convert<Gray, Byte>().Resize(240, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        timer2.Stop();
                        accountFaceID = result.Bytes;
                        CheckedIcon.Visibility = Visibility.Visible;
                        LoadingIcon.Visibility = Visibility.Hidden;
                        ImageBehavior.SetRepeatBehavior(CheckedIcon, new RepeatBehavior(TimeSpan.FromMilliseconds(500)));
                        saveButton.IsEnabled = true;
                        camera.Dispose();
                        break;

                    }
                }
                return;
            }
            catch (Exception)
            {
                camera.Dispose();
                return;
            }
        }


        private void faceDetectionResult(object sender, EventArgs e)
        {
            
            ErrorLabel.Text = "Face Could Not Be Recognized! Please Get More Close To The Camera";
            ErrorLabel.TextWrapping = TextWrapping.Wrap;

            timer2.Stop();
            LoadingIcon.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Visible;
            ErrorLabel.Visibility = Visibility.Visible;
            camera.Dispose();
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
                
                accountProfileImage = System.IO.File.ReadAllBytes(fullPath);


                image.ImageSource = bitmapImage;
                ProfileImage.Fill = image;

            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            account = new Account();
            account.First_Name = firstNameTextBox.Text;
            account.Last_Name = lastNameTextBox.Text;
            account.BirthDate = (DateTime) birthdateDataPicker.SelectedDate;
            ComboBoxItem comboBoxItem = (ComboBoxItem) accountTypeComboBox.SelectedItem;
            account.Privilege = comboBoxItem.Content.ToString();
            account.is_Active = false;
            account.face_ID = accountFaceID;
            account.Password = passwordTextBox.Text;
            myTransitioner.SelectedIndex = 1;

        }

        private void profileImageSaveButton_click(object sender, RoutedEventArgs e)
        {
            if(accountProfileImage != null)
            {
                account.profile_image = accountProfileImage;
            }

            initializeAccountSlide();

            if (account.Privilege.Equals("ADMIN"))
            {
                myTransitioner.SelectedIndex = 2;
            }
            else
            {  
                myTransitioner.SelectedIndex = 3;
            }

        }

        private void initializeAccountSlide()
        {
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            passwordTextBox.Text = "";
            profileImagePath.Text = "Click here to choose your profile image";

            LoadingIcon.Visibility = Visibility.Hidden;
            CheckedIcon.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Visible; ;
            ErrorLabel.Visibility = Visibility.Hidden;

        }

        private void backToLogin_click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void SaveAdminAccount_Click(object sender, RoutedEventArgs e)
        {
            House house = new House();
            house.id_Admin = account.Id;
            house.owner_first_name = OwnerFirstNameTextBox.Text;
            house.owner_last_name = OwnerLastNameTextBox.Text;
            ComboBoxItem comboBoxItem = (ComboBoxItem) PurshaseTypeComboBox.SelectedItem;
            house.type = comboBoxItem.Content.ToString();
            house.descriptive_name = DescriptiveNameTextBox.Text;
            house.address = AddressTextBox.Text;
            house.house_key = Guid.NewGuid().ToString();
            accountService.createAdminAccount(account, house);
            backToLogin_click(sender, e);
        }

        private void SaveResidentAccount_click(object sender, RoutedEventArgs e)
        {
            House house = accountService.getHouseByKey(HouseUUID.Text);
            if (house != null)
            {
                account.id_house = house.id;
                accountService.createResidentAccount(account);
                backToLogin_click(sender, e);
            }
        }
    }
}
