using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using CallLogger;

namespace Wpf_Calllogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Logger callLogger = new();
        DispatcherTimer timerSw = new DispatcherTimer();
        int secondsElapsed;

       

        public MainWindow()
        {
            InitializeComponent();
            status.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>();
            //callLogger.CallList = new();
            //callLog.Text = callLogger.CallList.ToString();
            dateTop.Content = DateTime.Now;
            timerSw.Interval = TimeSpan.FromSeconds(1);
            timerSw.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            timer.Content = (secondsElapsed / 100F).ToString("00:00 minutes");
        }

        private void Button_ClickSaveList(object sender, RoutedEventArgs e)
        {
            callLogger.Save();
        }

        private void Button_SaveCall(object sender, RoutedEventArgs e)
        {
            int number;
            bool success = int.TryParse(tid.Text, out number);
            Status state;
            bool success2 = Enum.TryParse(status.Text, out state);
               
            timerSw.Stop();
            string duration = (secondsElapsed / 100F).ToString("00:00 minutes");

            
            Call newCall = new(caller.Text, title.Text, desc.Text, number , state , duration);
            callLogger.AddCall(newCall);
            list.ItemsSource = callLogger.CallList;
            caller.Text = "";
            title.Text = "";
            desc.Text = "";
            tid.Text = "";
            status.Text = "";
            newCall = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            callLogger.startDay();
            list.ItemsSource = callLogger.CallList;
        }

        private void Button_ClickLoad(object sender, RoutedEventArgs e)
        {
            
            string date = loadDate.Text;
            callLogger.Load(date);
            list.ItemsSource = callLogger.CallList;
            dateTop.Content = callLogger.CallList[0].Date;
            loadDate.Text = "";
        }

        private void Button_ClickEndDay(object sender, RoutedEventArgs e)
        {
            callLogger.endDay();
            list.ItemsSource = callLogger.CallList;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timerSw.Start();
            secondsElapsed = 0;
        }


    }
}
