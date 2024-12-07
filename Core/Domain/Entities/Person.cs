using EventTicketingApp.Core.Domain.Enums;
using System.Reflection;

namespace EventTicketingApp.Core.Domain.Entities
{
    public abstract class Person
    {
        public string FullName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
    }
}
