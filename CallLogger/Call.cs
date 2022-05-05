using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLogger
{
    /// <summary>
    /// A class to store all the information of an incoming Call
    /// </summary>
    public class Call
    {
        private string caller = "Caller";
        private string title = "Title";
        private string description = "Description";
        private int ticketID = 0;
        private Status status;
        private DateTime date;
        private string duration;

        public string Caller { get { return caller; } set { caller = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Description { get { return description; } set { description = value; } }
        public int TicketID { get { return ticketID; } set { ticketID = value; } }
        public Status Status { get { return status; } set{ status = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public string Duration { get { return duration; } set { duration = value; } }


        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="caller">Who called?</param>
        /// <param name="title">What is it about?</param>
        /// <param name="desc">Short description / notes</param>
        /// <param name="id">Ticket ID</param>
        /// <param name="status">Status</param>
        /// <param name="duration">Total duration of the call</param>
        public Call(string caller, string title, string desc, int id, Status status, string duration)
        {
            Caller = caller;
            Title = title;
            Description = desc;
            TicketID = id;
            Status = status;
            Date = DateTime.Now;
            Duration = duration;
        }

        /// <summary>
        /// Constructor to use when importing .json files to the calllist
        /// </summary>
        public Call() { }
    }
}
