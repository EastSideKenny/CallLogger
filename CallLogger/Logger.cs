using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace CallLogger
{
    public class Logger
    {
        private List<Call> callList;
        private int count;
        public DateTime dateTime;

        public int Count { get { return count; } set { count = value; } }
        public List<Call> CallList { get { return callList; } set { callList = value; } }

        public void AddCall(Call call)
        {
            CallList.Add(call);
            count++;
        }

        public void Save()
        {
            string fileName = dateTime.ToString("yyyy-MM-dd");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(CallList, options);
            using (StreamWriter sw = new StreamWriter(@"C:\EBTJAJ\Calls\" + fileName + ".json"))
            {
                sw.Write(json);
            };
        }

        public void Load(string date)
        {
            string fileName = date;
            string path = @"C:\EBTJAJ\Calls\" + fileName + ".json";
            string jsonString = File.ReadAllText(path);
            CallList = new();
            CallList = JsonSerializer.Deserialize<List<Call>>(jsonString);
            dateTime = CallList[0].Date;
            Count = CallList.Count();

        }

        public void startDay()
        {
            dateTime = DateTime.Now;
            callList = new();
            count = 0;
            Console.WriteLine($"Today's date is {dateTime.ToString("dd/MM/yyyy")}");
        }

        public void endDay()
        {
            string fileName = dateTime.ToString("yyyy-MM-dd");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(CallList, options);
            using (StreamWriter sw = new StreamWriter(@"C:\EBTJAJ\Calls\" + fileName + ".json"))
            {
                sw.Write(json);
            };
            CallList = new();
        }

        public void ClearView()
        {
            dateTime = DateTime.Now;
            CallList = new();
        }

    }
}
