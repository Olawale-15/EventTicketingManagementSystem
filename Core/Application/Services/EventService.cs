using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Infrastructure.Context;
using EventTicketingApp.Infrastructure.Repositories;
using EventTicketingApp.Models;
using EventTicketingApp.Models.AttendeeModel;
using EventTicketingApp.Models.AttendeeTicketRecord;
using EventTicketingApp.Models.EventModell;
using EventTicketingApp.Models.TicketModel;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EventTicketingApp.Core.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventOrganizerService _eventOrganizerService;
        private readonly IEventOrganizerRepository _eventOrganizerRepository;
        private readonly EventTicketingAppContext _eventTicketingAppContext;
        private readonly IMailServices _emailSender;
        private readonly IAttendeeTicketRecordRepository _attendeeTicketRecordRepository;
        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork, IAttendeeRepository attendeeRepository, IEventOrganizerService eventOrganizerService, IEventOrganizerRepository eventOrganizerRepository, IMailServices emailSender, IAttendeeTicketRecordRepository attendeeTicketRecordRepository)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _attendeeRepository = attendeeRepository;
            _eventOrganizerService = eventOrganizerService;
            _eventOrganizerRepository = eventOrganizerRepository;
            _emailSender = emailSender;
            _attendeeTicketRecordRepository = attendeeTicketRecordRepository;
        }

        public async Task<BaseResponse<EventResponse>> CreateEvent(EventRequest request)
        {
            DateTime requestEndDate = request.DateAndTime.Add(request.Duration);
            TimeSpan difference = request.DateAndTime - DateTime.Now;

            if(difference.Days < 7)
            {
                var _event = new Event
                {
                    Status = EventStatus.Rejected
                };
                return new BaseResponse<EventResponse>
                {
                    Message = "The requested time is too close.",
                    IsSuccessful = false
                };
            }

            if (request.DateAndTime <= DateTime.Now)
            {
                var _event = new Event
                {
                    Status = EventStatus.Rejected
                };
                return new BaseResponse<EventResponse>
                {
                    Message = "The requested start date is incorrect!.",
                    IsSuccessful = false
                };
            }

            var overlappingEvents = await _eventRepository.GetAsync(e =>
                e.Venue == request.Venue &&
                e.DateAndTime < requestEndDate &&
                e.EndDate > request.DateAndTime);

            if (overlappingEvents != null)
            {
                var _event = new Event
                {
                    Status = EventStatus.Rejected
                };
                return new BaseResponse<EventResponse>
                {
                    Message = "An event is already scheduled at this venue during the requested time.",
                    IsSuccessful = false
                };
            }

            DateTime recentEventTime = request.DateAndTime.Add(-TimeSpan.FromHours(1));
            var recentEvent = await _eventRepository.GetAsync(e =>
                e.Venue == request.Venue &&
                e.EndDate > recentEventTime);

            if (recentEvent != null && request.Type == EventType.Offline)
            {
                var _event = new Event
                {
                    Status = EventStatus.Rejected
                };
                return new BaseResponse<EventResponse>
                {
                    Message = "A recent event Scheduled too close to the requested start time. Please choose a later time.",
                    IsSuccessful = false
                };
            }

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                DateAndTime = request.DateAndTime,
                Category = request.Category,
                Duration = request.Duration,
                EventOrganizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId(),
                Tickets = new List<Ticket>(),
                Type = request.Type,
                Venue = request.Venue,
                Status = EventStatus.Approved,
                EndDate = requestEndDate,
            };

            var eventOrganizer = await _eventOrganizerRepository.GetAsync(newEvent.EventOrganizerId);
            if (eventOrganizer != null)
            {
                eventOrganizer.EventsCreated.Add(newEvent);
            }

            await _eventOrganizerRepository.UpdateAsync(eventOrganizer);
            await _eventRepository.AddAsync(newEvent);
            await _unitOfWork.SaveAsync();


            var eventResponse = new EventResponse
            {
                Id = newEvent.Id,
                EventOrganizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId()
            };

            return new BaseResponse<EventResponse>
            {
                Message = "Event Created Successfully",
                IsSuccessful = true,
                Value = eventResponse
            };
        }


        public async Task<BaseResponse<ICollection<EventResponse>>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();

            var eventResponses = events.Select(x => new EventResponse
            {
                Category = x.Category,
                DateAndTime = x.DateAndTime,
                Description = x.Description,
                Duration = x.Duration,
                Id = x.Id,
                Title = x.Title,
                Type = x.Type,
                Venue = x.Venue,
                Status = x.Status,
                EventOrganizerId = x.EventOrganizerId,
                Tickets = x.Tickets.Select(t => new TicketResponse
                {
                    Id = t.Id,
                    Type = t.Type,
                    EventId = t.EventId,
                    AttendeeId = t.AttendeeId ?? Guid.Empty,
                    QRCode = t.QRCode,
                    VerificationCode = t.VerificationCode,
                    TicketTypeCounts = new List<TicketTypeCount>
                {
                new TicketTypeCount
                {
                    Type = t.Type,
                    Count = t.Count,
                    Price = t.Price
                }
            }
                }).ToList()
            }).ToList();

            return new BaseResponse<ICollection<EventResponse>>
            {
                Message = "List Of Events",
                IsSuccessful = true,
                Value = eventResponses
            };
        }


        public async Task<BaseResponse<EventResponse>> GetEventAsync(Guid id)
        {
            var _event = await _eventRepository.GetAsync(id);
            if (_event == null)
            {
                return new BaseResponse<EventResponse>
                {
                    Message = "Event Not Found.",
                    IsSuccessful = false
                };
            }

            var eventResponse = new EventResponse
            {
                Id = _event.Id,
                Category = _event.Category,
                DateAndTime = _event.DateAndTime,
                Description = _event.Description,
                Duration = _event.Duration,
                EventOrganizerId = _event.EventOrganizerId,
                Status = _event.Status,
                Title = _event.Title,
                Type = _event.Type,
                Venue = _event.Venue,
                Tickets = _event.Tickets.Select(t => new TicketResponse
                {
                    Id = t.Id,
                    Type = t.Type,
                    EventId = t.EventId,
                    AttendeeId = t.AttendeeId ?? Guid.Empty,
                    QRCode = t.QRCode,
                    VerificationCode = t.VerificationCode,
                    TicketTypeCounts = new List<TicketTypeCount>
                {
                new TicketTypeCount
                {
                    Type = t.Type,
                    Count = t.Count,
                    Price = t.Price
                }
            }
                }).ToList()
            };

            return new BaseResponse<EventResponse>
            {
                Message = "Event Found Successfully",
                IsSuccessful = true,
                Value = eventResponse
            };
        }


        public async Task<BaseResponse<ICollection<EventResponse>>> GetEventsByEventOrganizerId(Guid id)
        {
            var events = await _eventRepository.GetAllAsync(a => a.EventOrganizerId == id);

            var eventResponses = events.Select(x => new EventResponse
            {
                Category = x.Category,
                DateAndTime = x.DateAndTime,
                Description = x.Description,
                Duration = x.Duration,
                Id = x.Id,
                Title = x.Title,
                Type = x.Type,
                Venue = x.Venue,
                Status = x.Status,
                EventOrganizerId = x.EventOrganizerId,
                Tickets = x.Tickets.Select(t => new TicketResponse
                {
                    Id = t.Id,
                    Type = t.Type,
                    EventId = t.EventId,
                    EventName = t.Event.Title,
                    AttendeeId = t.AttendeeId ?? Guid.Empty,
                    QRCode = t.QRCode,
                    VerificationCode = t.VerificationCode,
                    TicketTypeCounts = new List<TicketTypeCount>
                {
                new TicketTypeCount
                {
                    Type = t.Type,
                    Count = t.Count,
                    Price = t.Price
                }
            }
                }).ToList() 
            }).ToList();

            return new BaseResponse<ICollection<EventResponse>>
            {
                Message = "List Of Events Managed by Event Organizer",
                IsSuccessful = true,
                Value = eventResponses
            };
        }


        public async Task<BaseResponse> RemoveEventAsync(Guid id)
        {
            var _event = await _eventRepository.GetAsync(id);
            if (_event == null)
            {
                return new BaseResponse
                {
                    Message = "Event Not Found",
                    IsSuccessful = false
                };
            }

            _event.Status = EventStatus.Cancelled;
            await _unitOfWork.SaveAsync();

            await _eventRepository.RemoveAsync(_event);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Event Successfully Cancelled",
                IsSuccessful = true
            };
        }

        public async Task<BaseResponse> UpdateEventAsync(Guid id, UpdateEventRequest request)
        {
            var _event = await _eventRepository.GetAsync(id);
            if (_event == null)
            {
                return new BaseResponse
                {
                    Message = "Event Not Found",
                    IsSuccessful = false
                };
            }

            var daysUntilEvent = (_event.DateAndTime - DateTime.UtcNow).TotalDays;
            if (daysUntilEvent <= 7)
            {
                return new BaseResponse
                {
                    Message = "Event cannot be rescheduled within 7 days of the event date.",
                    IsSuccessful = false
                };
            }

            var endDateTime = request.DateAndTime.Add(request.Duration);
            var recentTimeThreshold = request.DateAndTime.Add(-TimeSpan.FromHours(1));

            var overlappingEvents = await _eventRepository.GetAsync(e =>
                e.Venue == request.Venue &&
                e.Id != id &&  
                e.DateAndTime < endDateTime &&
                e.EndDate > request.DateAndTime
            );

            var recentEvent = await _eventRepository.GetAsync(e =>
                e.Venue == request.Venue &&
                e.Id != id && 
                e.EndDate > recentTimeThreshold
            );

            if (overlappingEvents != null || recentEvent != null)
            {
                return new BaseResponse
                {
                    Message = "An event is already scheduled at this venue during the requested time.",
                    IsSuccessful = false
                };
            }

            _event.Title = request.Title ?? _event.Title;
            _event.Status = EventStatus.Rescheduled;
            _event.Type = request.Type;
            _event.DateAndTime = request.DateAndTime;
            _event.Description = request.Description ?? _event.Description;
            _event.Duration = request.Duration;
            _event.Venue = request.Venue ?? _event.Venue;

            await _eventRepository.UpdateAsync(_event);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Event Rescheduled Successfully",
                IsSuccessful = true
            };
        }

        public async Task<BaseResponse<ICollection<AttendeeResponse>>> GetAttendeesByEventIdAsync(Guid eventId)
        {
            var attendees = await _attendeeRepository.GetByEventIdAsync(eventId);
            if (attendees != null && attendees.Any())
            {
                var attendeeResponses = new List<AttendeeResponse>();

                foreach (var attendee in attendees)
                {
                    var ticketRecords = await _attendeeTicketRecordRepository.GetByAttendeeIdAsync(attendee.Id);

                    var attendeeTicketRecordResponses = ticketRecords.Select(record => new AttendeeTicketRecordResponse
                    {
                        Id = record.Id,
                        AttendeeId = record.AttendeeId,
                        AttendeeName = record.Attendee.FullName,
                        EventId = record.EventId,
                        EventTitle = record.Event.Title,
                        Type = record.Ticket.Type,
                        Count = record.TicketCount,
                        TotalPrice = record.TotalPrice
                    }).ToList();

                    var attendeeResponse = new AttendeeResponse
                    {
                        Id = attendee.Id,
                        UserName = attendee.User.UserName,
                        Email = attendee.User.Email,
                        FullName = attendee.FullName,
                        Address = attendee.Address,
                        PhoneNumber = attendee.PhoneNumber,
                        Password = attendee.User.PasswordHash,
                        UserId = attendee.UserId,
                        Tickets = attendee.Tickets.Where(t => t.EventId == eventId).Select(t => new TicketResponse
                        {
                            Id = t.Id,
                            Type = t.Type,
                            EventId = t.EventId,
                            AttendeeId = (Guid)t.AttendeeId,
                            QRCode = t.QRCode,
                            VerificationCode = t.VerificationCode,
                            TicketTypeCounts = new List<TicketTypeCount>
                    {
                        new TicketTypeCount
                        {
                            Type = t.Type,
                            Count = t.Count,
                            Price = t.Price
                        }
                    }
                        }).ToList(),
                        TicketRecords = attendeeTicketRecordResponses
                    };

                    attendeeResponses.Add(attendeeResponse);
                }

                return new BaseResponse<ICollection<AttendeeResponse>>
                {
                    IsSuccessful = true,
                    Value = attendeeResponses,
                    Message = "Attendees retrieved successfully."
                };
            }

            return new BaseResponse<ICollection<AttendeeResponse>>
            {
                IsSuccessful = false,
                Message = "No attendees found for the event."
            };
        }

        public async Task<IEnumerable<EventResponse>> GetAllEventsAsync(string searchQuery)
        {
            var events = await _eventRepository.GetAllAsync();

            var upcomingEvents = events.Where(ev => ev.DateAndTime >= DateTime.Now);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                upcomingEvents = events
                    .Where(e => e.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                e.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                e.Venue.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            var eventResponses = upcomingEvents.Select(e => new EventResponse
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Venue = e.Venue,
                Category = e.Category,
                Duration = e.Duration,
                Status = e.Status,
                Type = e.Type,
                EventOrganizerId = e.EventOrganizerId,
                DateAndTime = e.DateAndTime,
                Tickets = e.Tickets
                    .GroupBy(t => t.Type)
                    .Select(g => new TicketResponse
                    {
                        Id = Guid.NewGuid(),
                        Type = g.Key,
                        EventId = e.Id,
                        EventName = e.Title,
                        AttendeeId = Guid.Empty, 
                        QRCode = null,
                        VerificationCode = null,
                        TicketTypeCounts = new List<TicketTypeCount>
                        {
                    new TicketTypeCount
                    {
                        Type = g.Key,
                        Count = g.Sum(t => t.Count),
                        Price = g.First().Price
                    }
                        }
                    }).ToList() ?? new List<TicketResponse>()
            }).ToList();

            return eventResponses;
        }

        public async Task<BaseResponse<EventResponse>> GetEventByTitleAsync(string title)
        {
            var _event = await _eventRepository.GetAsync(a => a.Title == title);
            if (_event == null)
            {
                return new BaseResponse<EventResponse>
                {
                    Message = "Event Not Found.",
                    IsSuccessful = false
                };
            }

            var eventResponse = new EventResponse
            {
                Id = _event.Id,
                Category = _event.Category,
                DateAndTime = _event.DateAndTime,
                Description = _event.Description,
                Duration = _event.Duration,
                EventOrganizerId = _event.EventOrganizerId,
                Status = _event.Status,
                Title = _event.Title,
                Type = _event.Type,
                Venue = _event.Venue,
                Tickets = _event.Tickets.Select(t => new TicketResponse
                {
                    Id = t.Id,
                    Type = t.Type,
                    EventId = t.EventId,
                    AttendeeId = t.AttendeeId ?? Guid.Empty,
                    QRCode = t.QRCode,
                    VerificationCode = t.VerificationCode,
                    TicketTypeCounts = new List<TicketTypeCount>
                {
                new TicketTypeCount
                {
                    Type = t.Type,
                    Count = t.Count,
                    Price = t.Price
                }
            }
                }).ToList()
            };

            return new BaseResponse<EventResponse>
            {
                Message = "Event Found Successfully",
                IsSuccessful = true,
                Value = eventResponse
            };
        }

        public async Task<BaseResponse> SendCancellationEmailsAsync(Guid eventId, ICollection<AttendeeResponse> attendees)
        {
            var eventEntity = await _eventRepository.GetAsync(eventId);
            if (eventEntity == null)
            {
                throw new Exception("Event not found.");
            }
            foreach (var attendee in attendees)
            {
                var emailContent = $"Dear {attendee.FullName},\n\nWe regret to inform you that the event ({eventEntity.Title}) you purchased a ticket for has been cancelled. The total amount has been refunded to your wallet. We apologize for any inconvenience caused.\n\nBest regards,\nEvent Management Team";

                var emailRequest = new EmailDto
                {
                    ToEmail = attendee.Email,
                    Subject = "Event Cancellation Notification",
                    HtmlContent = emailContent,
                    ToName = attendee.FullName,
                };

                _emailSender.SendEMail(emailRequest);
            }

            return new BaseResponse
            {
                IsSuccessful = true,
                Message = "Emails sent successfully to all attendees."
            };
        }

        public async Task<BaseResponse> NotifyAttendeesOnEventUpdateAsync(Guid eventId)
        {
            var eventEntity = await _eventRepository.GetAsync(eventId);
            if (eventEntity == null)
            {
                throw new Exception("Event not found.");
            }

            var attendees = await _attendeeRepository.GetAttendeesByEventIdAsync(eventId);
            if (attendees == null || !attendees.Any())
            {
                return new BaseResponse
                {
                    IsSuccessful = false,
                    Message = "No attendees found for this event."
                };
            }

            string subject = $"Update on the event: {eventEntity.Title}";
            string bodyTemplate = $@"
            <p>Dear [AttendeeName],</p>
            <p>We would like to inform you that the event <strong>{eventEntity.Title}</strong> has been updated. Please review the updated details to stay informed about any changes.</p>
            <p>Event Date and Time: {eventEntity.DateAndTime.ToString("f")}</p>
            <p>Venue: {eventEntity.Venue}</p>
            <p>Please go back to the event page to see the updated information.</p>
            <p>Thank you for your understanding.</p>
            <p>Best regards,</p>
            <p>The Event Team</p>";

            foreach (var attendee in attendees)
            {
                string body = bodyTemplate.Replace("[AttendeeName]", attendee.FullName); 

                var emailDto = new EmailDto
                {
                    ToEmail = attendee.User.Email,
                    ToName = attendee.FullName,
                    Subject = subject,
                    HtmlContent = body
                };

                _emailSender.SendEMail(emailDto);  
            }

            return new BaseResponse
            {
                IsSuccessful = true,
                Message = "Emails sent successfully to all attendees."
            };
        }


        public async Task<BaseResponse<ICollection<AttendeeResponse>>> GetAttendeessByEventIdAsync(Guid eventId)
        {
            // Retrieve all attendees associated with the event
            var attendees = await _attendeeRepository.GetAttendeesByEventIdAsync(eventId);

            if (attendees != null && attendees.Any())
            {
                var attendeeResponses = new List<AttendeeResponse>();

                foreach (var attendee in attendees)
                {
                    // Fetch ticket records for the attendee
                    var ticketRecords = await _attendeeTicketRecordRepository.GetByAttendeeIdAsync(attendee.Id);

                    // Map each ticket record to its response model
                    var attendeeTicketRecordResponses = ticketRecords.Select(record => new AttendeeTicketRecordResponse
                    {
                        Id = record.Id,
                        AttendeeId = record.AttendeeId,
                        AttendeeName = record.Attendee.FullName,
                        EventId = record.EventId,
                        EventTitle = record.Event.Title,
                        Type = record.Ticket.Type,
                        Count = record.TicketCount,
                        TotalPrice = record.TotalPrice
                    }).ToList();

                    // Map the attendee to its response model
                    var attendeeResponse = new AttendeeResponse
                    {
                        Id = attendee.Id,
                        UserName = attendee.User.UserName,
                        Email = attendee.User.Email,
                        FullName = attendee.FullName,
                        Address = attendee.Address,
                        PhoneNumber = attendee.PhoneNumber,
                        Password = attendee.User.PasswordHash,
                        UserId = attendee.UserId,
                        Tickets = attendee.Tickets
                            .Where(t => t.EventId == eventId)
                            .Select(t => new TicketResponse
                            {
                                Id = t.Id,
                                Type = t.Type,
                                EventId = t.EventId,
                                AttendeeId = t.AttendeeId ?? Guid.Empty,
                                QRCode = t.QRCode,
                                VerificationCode = t.VerificationCode,
                                TicketTypeCounts = new List<TicketTypeCount>
                                {
                            new TicketTypeCount
                            {
                                Type = t.Type,
                                Count = t.Count,
                                Price = t.Price
                            }
                                }
                            }).ToList(),
                        TicketRecords = attendeeTicketRecordResponses
                    };

                    attendeeResponses.Add(attendeeResponse);
                }

                // Return the response with the list of attendee responses
                return new BaseResponse<ICollection<AttendeeResponse>>
                {
                    IsSuccessful = true,
                    Value = attendeeResponses,
                    Message = "Attendees retrieved successfully."
                };
            }

            // Return a response indicating no attendees were found for the event
            return new BaseResponse<ICollection<AttendeeResponse>>
            {
                IsSuccessful = false,
                Message = "No attendees found for the event."
            };
        }

    }
}

