﻿using System;
namespace Ticket.Presentation.ViewModels
{
    public class OpenSubTicketsReportViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TicketId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NameSurname { get; set; }
        public string CardName { get; internal set; }
    }
}
