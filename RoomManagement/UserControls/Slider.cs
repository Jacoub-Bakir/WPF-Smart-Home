using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoomManagement.UserControls
{
    public class Slider : TransitionerSlide
    {
        
        
        static Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider), new FrameworkPropertyMetadata(typeof(Slider)));
        }
        
        
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Slider), new PropertyMetadata(""));

        public string RoomID
        {
            get { return (string)GetValue(RoomIDProperty); }
            set { SetValue(RoomIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoomIDProperty =
            DependencyProperty.Register("RoomID", typeof(string), typeof(Slider), new PropertyMetadata(""));


        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(Slider), new PropertyMetadata(""));

        public byte[] ImageSourceAsBytes
        {
            get { return (byte[])GetValue(ImageSourceAsBytesProperty); }
            set { SetValue(ImageSourceAsBytesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceAsBytesProperty =
            DependencyProperty.Register("ImageSourceAsBytes", typeof(byte[]), typeof(Slider), new PropertyMetadata(null));



        public string Temperature
        {
            get { return (string)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("Temperature", typeof(string), typeof(Slider), new PropertyMetadata(""));

        public string Humidity
        {
            get { return (string)GetValue(HumidityProperty); }
            set { SetValue(HumidityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HumidityProperty =
            DependencyProperty.Register("Humidity", typeof(string), typeof(Slider), new PropertyMetadata(""));
        
        public ObservableCollection<RoomDevice> RoomDevicesList
        {
            get { return (ObservableCollection<RoomDevice>)GetValue(RoomDevicesListProperty); }
            set { SetValue(RoomDevicesListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoomDevicesList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoomDevicesListProperty =
            DependencyProperty.Register("RoomDevicesList", typeof(ObservableCollection<RoomDevice>), typeof(Slider), new PropertyMetadata(null));
    } 
}
