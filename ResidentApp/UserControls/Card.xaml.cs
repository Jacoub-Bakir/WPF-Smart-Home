using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ResidentApp.UserControls
{
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Card));
        
        public int Card_ID
        {
            get { return (int)GetValue(Card_IDProperty); }
            set { SetValue(Card_IDProperty, value); }
        }

        public static readonly DependencyProperty Card_IDProperty = DependencyProperty.Register("Card_ID", typeof(int), typeof(Card));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(Card));


        public bool IsHorizontal
        {
            get { return (bool)GetValue(IsHorizontalProperty); }
            set { SetValue(IsHorizontalProperty, value); }
        }

        public static readonly DependencyProperty IsHorizontalProperty = DependencyProperty.Register("IsHorizontal", typeof(bool), typeof(Card));


        public ImageSource ImageOn
        {
            get { return (ImageSource)GetValue(ImageOnProperty); }
            set { SetValue(ImageOnProperty, value); }
        }

        public static readonly DependencyProperty ImageOnProperty = DependencyProperty.Register("ImageOn", typeof(ImageSource), typeof(Card));


        public ImageSource ImageOff
        {
            get { return (ImageSource)GetValue(ImageOffProperty); }
            set { SetValue(ImageOffProperty, value); }
        }

        public static readonly DependencyProperty ImageOffProperty = DependencyProperty.Register("ImageOff", typeof(ImageSource), typeof(Card));

        private void deviceState_changed(object sender, RoutedEventArgs e)
        {
            var deviceCard = e.Source as FrameworkElement;
            if(deviceCard != null)
            {
                string device_id = deviceCard.Uid;
                MainWindow.deviceStateChanged(Convert.ToInt32(device_id), IsChecked);
            }
        }
    }
}