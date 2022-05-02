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
using CallLogger;

namespace Wpf_Calllogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Logger callLogger = new();
        

        public MainWindow()
        {
            InitializeComponent();
            status.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>();
            //callLogger.CallList = new();
            //callLog.Text = callLogger.CallList.ToString();
            dateTop.Content = DateTime.Now;



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
            
            Call newCall = new(caller.Text, title.Text, desc.Text, number , state);
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
        }

        private void Button_ClickEndDay(object sender, RoutedEventArgs e)
        {
            callLogger.endDay();
            list.ItemsSource = callLogger.CallList;
        }
    }
}
