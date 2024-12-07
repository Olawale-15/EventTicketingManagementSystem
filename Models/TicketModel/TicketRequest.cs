using EventTicketingApp.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventTicketingApp.Models.TicketModel
{
    public class CreateTicketRequest
    {
        public Guid EventId { get; set; } = default!;
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Count is required")]
        public int Count { get; set; } = default!;
        public TicketType Type { get; set; } = default!;
    }

    public class UpdateTicketRequest
    {
        public Guid EventId { get; set; }
        public Guid TicketId { get; set; } = default!;
        public decimal Price { get; set; }
        public int Count { get; set; } = default!;
        public TicketType Type { get; set; } = default!;
    }

    public class BuyTicketRequest
    {
        public Guid EventId { get; set; } = default!;
        public int Count { get; set; } = default!;
        public TicketType Type { get; set; } = default!;
    }
}
