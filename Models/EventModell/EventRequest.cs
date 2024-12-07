using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventTicketingApp.Models.EventModell
{
    public class EventRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Venue is required")]
        public string Venue { get; set; } = default!;
        [Required(ErrorMessage = "Category is required")]
        public EventCategory Category { get; set; } = default!;
        [Required(ErrorMessage = "Type is required")]
        public EventType Type { get; set; } = default!;
        [Required(ErrorMessage = "Date and time are required")]
        public DateTime DateAndTime { get; set; } = default!;
        [Required(ErrorMessage = "Duration is required")]
        public TimeSpan Duration { get; set; } = default!;
        public Guid EventOrganizerId { get; set; } = default!;
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }


    public class UpdateEventRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Venue { get; set; } = default!;
        public DateTime DateAndTime { get; set;} = default!;
        public TimeSpan Duration { get; set; } = default!;
        public EventType Type { get; set; } = default!;
    }

    public class EventSearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
    }

    public class EventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }

    public class EventDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string Venue { get; set; } = default!;
        public EventCategory Category { get; set; } = default!;
        public EventType Type { get; set; } = default!;
        public EventStatus Status { get; set; } = default!;
        public DateTime DateAndTime { get; set; } = default!;
        public TimeSpan Duration { get; set; } = default!;
        public DateTime EndDate { get; set; } = default!;
        public string OrganizerName { get; set; } = default!; // Assuming Organizer has a Name property
        public List<TicketViewModel> Tickets { get; set; } = new List<TicketViewModel>();
    }


    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public TicketType Type { get; set; } 
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
