using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using RemoteInerfaces.Services;
using RemoteInerfaces.Entities;

namespace SignIn_Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Boolean isGust = false;
        bool not_active = false;
        int house_id = 1;
        Boolean requestWaiting = false;
        DispatcherTimer timer;
        DispatcherTimer timer2;
        DispatcherTimer timer3;
        DispatcherTimer timer4;
        HaarCascade faceDetected;
        Image<Bgr, Byte> frame;
        Capture camera;
        Image<Gray, byte> result;
        Image<Gray, byte> grayFace = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        string name = null;
        Account userAccount = null;
        IAccountService accountService;

        public LoginWindow()
        {
            accountService = (IAccountService) Activator.GetObject(typeof(IAccountService), "tcp://localhost:8080/AccountService");
            getFaceIDFromDB();
            InitializeComponent();
        }

        public void getFaceIDFromDB()
        {
            faceDetected = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                
                List<FaceID> trainingImagesList = accountService.getAccountFacesWithIDs();
                foreach (FaceID faceID in trainingImagesList)
                {
                    Console.WriteLine(faceID.id);
                    Image<Gray, byte> depthImage = new Image<Gray, byte>(240, 240);
                    depthImage.Bytes = faceID.face_id;
                    trainingImages.Add(depthImage);
                    labels.Add(faceID.id.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Nothing in the Database");
            }
        }

        private void FrameProcedure()
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
                        if (trainingImages.ToArray().Length != 0)
                        {
                            MCvTermCriteria termCriterias = new MCvTermCriteria(trainingImages.Count, 0.001);
                            EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 1500, ref termCriterias);
                            name = recognizer.Recognize(result);

                            // face saved on database 
                            if (!name.Equals(""))
                            {
                                timer2.Stop();
                                userAccount = accountService.getAccount(Convert.ToInt32(name));
                                if (!userAccount.is_Active)
                                {
                                    camera.Dispose();
                                    LoadingIcon.Visibility = Visibility.Hidden;
                                    not_active = true;
                                    detectionResult();
                                }
                                CheckedIcon.Visibility = Visibility.Visible;
                                LoadingIcon.Visibility = Visibility.Hidden;

                                UserNameWelcoming.Text = "Welcome Mr. " + userAccount.First_Name;

                                ImageBehavior.SetRepeatBehavior(CheckedIcon, new RepeatBehavior(TimeSpan.FromMilliseconds(500)));
                                timer = new DispatcherTimer();
                                timer.Interval = TimeSpan.FromSeconds(1);
                                timer.Tick += goToPasswordSlide;
                                timer.Start();
                                camera.Dispose();
                                break;
                                
                            }
                        }
                    }
                }
                return;
            }
            catch (Exception e)
            {
                camera.Dispose();
                return;
            }
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
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
            
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new Action(FrameProcedure));
            
           

        }

        private void guestButton_Click(object sender, RoutedEventArgs e)
        {
            // SignUp window
            new MainWindow().Show();
            this.Close();
        }


        private void openDoorButton_click(object sender, RoutedEventArgs e)
        {
            if (userAccount != null)
            {
                if (userAccount.Password == passwordBox.Password)
                {
                    passwordBox.Password = "";
                 
                    if (userAccount.Privilege == "ADMIN")
                    {
                        // go to admin app
                        new RoomManagement.MainWindow(userAccount.Id).Show();
                        Close();
                    }
                    else if(userAccount.Privilege == "RESIDENT" && userAccount.is_Active)
                    {
                       
                        // go to resident  app
                        new ResidentApp.MainWindow(userAccount.Id ).Show();
                        Close();
                    }
                }
                else
                {
                    ErrorLabel2.Text = "Wrong Password! Who Are You?";
                    ErrorLabel2.Visibility = Visibility.Visible;
                }
            }
        }

      

       

        private void goBackToHomeSlide(object sender, EventArgs e)
        {
            timer3.Stop();
            userAccount = null;
            name = null;
            myTransitioner.SelectedIndex = 0;
        }

        private void goToPasswordSlide(object sender, EventArgs e)
        {
            timer.Stop();
            CheckedIcon.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Visible;
            myTransitioner.SelectedIndex = 1;

        }

        private void detectionResult()
        {
            if (!not_active) { 
                if (isGust)
                {
                    ErrorLabel.Text = "Face Could Not Be Recognized! Please Get More Close To The Camera";
                    ErrorLabel.TextWrapping = TextWrapping.Wrap;
                }
                    else
                {
                    ErrorLabel.Text = "Face Not Recognized! Do You Have Account";
                }
            }
            else
            {
                ErrorLabel.Text = "Your Account is Not Active Yet Contact Your Admin";
                ErrorLabel.TextWrapping = TextWrapping.Wrap;
            }
            not_active = false;
            timer2.Stop();
            LoadingIcon.Visibility = Visibility.Hidden;
            Face_ID.Visibility = Visibility.Visible;
            ErrorLabel.Visibility = Visibility.Visible;
            checkButton.Content = "Try Again";
            camera.Dispose();
        }
        private void faceDetectionResult(object sender, EventArgs e)
        {
            detectionResult();
        }


        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }



        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

      
    }
}
