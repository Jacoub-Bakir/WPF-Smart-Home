using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace ResidentApp.UserControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ResidentApp.UserControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ResidentApp.UserControls;assembly=ResidentApp.UserControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Slider/>
    ///
    /// </summary>
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
