using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLogger
{
    public class Call
    {
        private string caller = "Caller";
        private string title = "Title";
        private string description = "Description";
        private int ticketID = 0;
        private Status status;

        public string Caller { get { return caller; } set { caller = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Description { get { return description; } set { description = value; } }
        public int TicketID { get { return ticketID; } set { ticketID = value; } }
        public Status Status { get { return status; } set{ status = value; } }

        public Call(string caller, string title, string desc, int id, Status status)
        {
            Caller = caller;
            Title = title;
            Description = desc;
            TicketID = id;
            Status = status;
        }
    }
}
