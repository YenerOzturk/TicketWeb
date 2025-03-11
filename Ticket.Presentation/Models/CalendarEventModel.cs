using System;
namespace Ticket.Presentation.Models
{
    public class CalendarEventModel
    {
        public int id { get; set; }
        public string className { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string description { get; set; }
        public bool isUpdate { get; set; }
    }
}
