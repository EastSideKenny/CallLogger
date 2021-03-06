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
using System.IO;


namespace Wpf_Calllogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Logger callLogger = new(); // instantiate Logger
        DispatcherTimer timerSw = new DispatcherTimer(); // instantiate timer
        int secondsElapsed; // keep track of seconds elapsed
        TimeSpan totalDurationSpan;


        public MainWindow()
        {
            InitializeComponent();
            status.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>(); // Get Enum Values for status dropdown
            //callLogger.CallList = new();
            //callLog.Text = callLogger.CallList.ToString();
            dateTop.Content = DateTime.Now; // set time to current
            timerSw.Interval = TimeSpan.FromSeconds(1); // set Ticker timespan
            timerSw.Tick += Timer_Tick;
        }

        /// <summary>
        /// Update Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            secondsElapsed++;
            timer.Content = TimeSpan.FromSeconds(secondsElapsed);
        }

        /// <summary>
        /// Save the current list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickSaveList(object sender, RoutedEventArgs e)
        {
            callLogger.Save();
            UpdateTotals();
        }

        /// <summary>
        /// Save a call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_SaveCall(object sender, RoutedEventArgs e)
        {
            int number;
            bool success = int.TryParse(tid.Text, out number); // make sure tid.Text is an int
            if (!success)
            {
                number = 000000;
            }
            Status state;
            bool success2 = Enum.TryParse(status.Text, out state); // make sure status.Text is a valid enum item   
            if (!success2) {
                state = Status.Open;
            }
            timerSw.Stop(); // stop the current timer
            string duration = TimeSpan.FromSeconds(secondsElapsed).ToString();

            // create note variable
            string note = "";

            if (textNotes.Text != String.Empty) // if the note is not empty - save it in a text file
            { 
                note = DateTime.Now.ToString("dd-MM-yyyy") + "-" + TimeOnly.FromDateTime(DateTime.Now).ToString("HHmm") + ".txt";

                using (StreamWriter writer = File.CreateText($"notes/{note}"))
                {
                    writer.Write(textNotes.Text);
                }                
            }

            Call newCall = new(caller.Text, title.Text, desc.Text, number , state , duration, note); // create a call object
            callLogger.AddCall(newCall); // add the object to the call list
            list.ItemsSource = callLogger.CallList; // refresh call list view

            // Reset the fields / timer / update totals
            resetFields();
            newCall = null!;
            secondsElapsed = 0;
            timer.Content = TimeSpan.FromSeconds(secondsElapsed);
            textNotes.Visibility = Visibility.Hidden;
            UpdateTotals();
            
        }

        /// <summary>
        /// Start a new day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            callLogger.startDay();
            list.ItemsSource = callLogger.CallList;
            resetFields();
            totalCalls.Text = "0";
            totalDuration.Content = "00:00:00";
        }

        /// <summary>
        /// Load a .json file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickLoad(object sender, RoutedEventArgs e)
        {
            
            string date = loadDate.Text;
            callLogger.Load(date);
            list.ItemsSource = callLogger.CallList;
            dateTop.Content = callLogger.CallList[0].Date;
            resetFields();
            UpdateTotals();
        }

        /// <summary>
        /// End current day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickEndDay(object sender, RoutedEventArgs e)
        {
            callLogger.endDay();
            list.ItemsSource = callLogger.CallList;
            resetFields();
            totalCalls.Text = "0";
            totalDuration.Content = "00:00:00";
        }

        /// <summary>
        /// Start a call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timerSw.Start();
            secondsElapsed = 0;
            resetFields();
        }


        /// <summary>
        /// Update total calls and total duration counters
        /// </summary>
        private void UpdateTotals()
        {
            totalCalls.Text = callLogger.CallList.Count().ToString();
            totalDurationSpan = new();
            foreach (Call call in callLogger.CallList)
            {
                totalDurationSpan += TimeSpan.Parse(call.Duration);
            }
            totalDuration.Content = totalDurationSpan; 
        }

        /// <summary>
        /// Reset all the fields 
        /// </summary>
        private void resetFields()
        {
           
            caller.Text = "";
            title.Text = "";
            desc.Text = "";
            tid.Text = "";
            status.Text = "";
            loadDate.Text = "";
            textNotes.Text = "";


        }

        /// <summary>
        /// Add a note to the current ongoing call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNote(object sender, RoutedEventArgs e)
        {
            if (textNotes.Visibility == Visibility.Hidden) { textNotes.Visibility = Visibility.Visible; }
            else if (textNotes.Visibility == Visibility.Visible) { textNotes.Visibility = Visibility.Hidden; }
            textNotes.Text = "";
        }

        /// <summary>
        /// Read a note from a saved call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void readNote(object sender, RoutedEventArgs e)
        {
            string noteToRead = loadNote.Text + ".txt";

            using (StreamReader sr = new StreamReader("notes/" + noteToRead))
            {
                textNotes.Text = sr.ReadToEnd();
                loadNote.Text = "";
            }

            textNotes.Visibility = Visibility.Visible;
        }
    }
}
