using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

namespace RoomManagement.UserControls
{
    public partial class ButtonAdd : UserControl
    {
        public ButtonAdd()
        {
            InitializeComponent();
            DataContext = this;
        }


        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Caption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(ButtonAdd), new PropertyMetadata(""));

        public string RoomID
        {
            get { return (string)GetValue(RoomIDProperty); }
            set { SetValue(RoomIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Caption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoomIDProperty =
            DependencyProperty.Register("RoomID", typeof(string), typeof(ButtonAdd), new PropertyMetadata(""));






    }
}