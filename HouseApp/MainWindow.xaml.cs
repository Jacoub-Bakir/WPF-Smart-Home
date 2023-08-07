using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace HouseApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer updateTimeTimer;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            dateLabel.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            updateTimeTimer = new DispatcherTimer();
            updateTimeTimer.Interval = TimeSpan.FromSeconds(1);
            updateTimeTimer.Tick += updateTimeTimer_tick;
            updateTimeTimer.Start();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += timer_tick;
            timer.Start();
            
        }

        private void updateTimeTimer_tick(object sender, EventArgs e)
        {
            dateLabel.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void timer_tick(object sender, EventArgs e)
        {
            RemoteInerfaces.Temperatur_HumidityInterface obj = (RemoteInerfaces.Temperatur_HumidityInterface)Activator.GetObject(typeof(RemoteInerfaces.Temperatur_HumidityInterface), "tcp://localhost:8081/Temperature_Humidity");

            tempLabel.Content = obj.getTemperature();
            humLabel.Content = obj.getHumidity();
        }
    }

}
