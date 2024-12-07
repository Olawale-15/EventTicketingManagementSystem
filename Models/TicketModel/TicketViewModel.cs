using EventTicketingApp.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventTicketingApp.Models.TicketModel
{
    public class TicketPurchaseViewModel
    {
        public string EventName { get; set; }
        public List<TicketTypeViewModel> TicketTypes { get; set; } = new List<TicketTypeViewModel>();
        public List<SelectedTicketViewModel> SelectedTickets { get; set; } = new List<SelectedTicketViewModel>();
    }

    public class TicketTypeViewModel
    {
        public TicketType Type { get; set; }
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
    }

    public class SelectedTicketViewModel
    {
        public TicketType Type { get; set; }
        public int Count { get; set; }
    }

}
