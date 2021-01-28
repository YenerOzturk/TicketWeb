﻿using System;
namespace Ticket.Presentation.ViewModels
{
    public class TicketDetailReport
    {
        public string TicketDetail { get; set; }
        public string SubTicketDetail { get; set; }
        public string CreatedBy { get; set; }
        public string WorkingBy { get; set; }
        public int WorkDay { get; set; }
        public int WorkHour { get; set; }
        public string StartDate { get; set; }
    }
}
