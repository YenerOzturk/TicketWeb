using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Ticket.Presentation.Models
{
    public class SubTicketModel
    {
        public string Description { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
        public int TicketId { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CloseTicket { get; set; }
        public bool AllDay { get; set; }
        public bool SendMail { get; set; }
        public bool SupportScope { get; set; }
        public int SupportType { get; set; }

        [JsonIgnore]
        public IFormFile File { get; set; }
        public string Attachment { get; set; }
    }
}
