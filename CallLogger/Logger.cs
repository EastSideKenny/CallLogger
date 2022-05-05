using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace CallLogger
{
    /// <summary>
    /// Class to log the calls in a list
    /// </summary>
    public class Logger
    {
        private ObservableCollection<Call> callList;
        private int count;
        public DateTime dateTime;

        public int Count { get { return count; } set { count = value; } }
        public ObservableCollection<Call> CallList { get { return callList; } set { callList = value; } }

        /// <summary>
        /// Add a call to the call list
        /// </summary>
        /// <param name="call">Call object</param>
        public void AddCall(Call call)
        {
            CallList.Add(call);
            count++;
        }

        /// <summary>
        /// Save the current list in .json format
        /// </summary>
        public void Save()
        {
            string fileName = dateTime.ToString("yyyy-MM-dd");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(CallList, options);
            using (StreamWriter sw = new StreamWriter(fileName + ".json"))
            {
                sw.Write(json);
            };
        }

        /// <summary>
        /// Load an existing .json file and import its content into the call list
        /// </summary>
        /// <param name="date">Filename is equal to a date</param>
        public void Load(string date)
        {
            string fileName = date + ".json";
            string jsonString = File.ReadAllText(fileName);
            CallList = new();
            CallList = JsonSerializer.Deserialize<ObservableCollection<Call>>(jsonString);
            dateTime = CallList[0].Date;
            Count = CallList.Count();

        }

        /// <summary>
        /// Sets the current time and creates a new call list
        /// </summary>
        public void startDay()
        {
            dateTime = DateTime.Now;
            callList = new();
            count = 0;
            Console.WriteLine($"Today's date is {dateTime.ToString("dd/MM/yyyy")}");
        }

        /// <summary>
        /// Saves  the current list in .json format and sets the call list back to zero
        /// </summary>
        public void endDay()
        {
            string fileName = dateTime.ToString("yyyy-MM-dd");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(CallList, options);
            using (StreamWriter sw = new StreamWriter(fileName + ".json")) // (@"C:\EBTJAJ\Calls\" +*/
            {
                sw.Write(json);
            };
            CallList = new();
        }

    }
}
