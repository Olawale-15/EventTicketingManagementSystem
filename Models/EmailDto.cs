﻿namespace EventTicketingApp.Models
{
    public class EmailDto
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
    }
}
