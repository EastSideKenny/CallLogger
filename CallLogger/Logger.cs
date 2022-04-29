using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLogger
{
    public class Logger
    {
        private List<Call> callList;
        private int count;

        public int Count { get { return count; } set { count = value; } }
        public List<Call> CallList { get { return callList; } set { callList = value; } }

        public void AddCall(Call call)
        {
            CallList.Add(call);
            count++;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void startDay()
        {
            callList = new();
            count = 0;
        }

        public void endDay()
        {
            throw new NotImplementedException();
        }


    }
}
