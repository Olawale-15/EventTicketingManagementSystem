using EventTicketingApp.Constant;
using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Application.Services;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Models.EventModell;
using EventTicketingApp.Models.TicketModel;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IWalletService _walletService;
        private readonly IAttendeeService _attendeeService;
        private readonly IEventRepository _eventRepository;
        private readonly IEventOrganizerService _eventOrganizerService;
        private readonly ITicketService _ticketService;
        public EventController(IEventService eventService, IWalletService walletService, IAttendeeService attendeeService, IEventRepository eventRepository, IEventOrganizerService eventOrganizerService, ITicketService ticketService)
        {
            _eventService = eventService;
            _walletService = walletService;
            _attendeeService = attendeeService;
            _eventRepository = eventRepository;
            _eventOrganizerService = eventOrganizerService;
            _ticketService = ticketService;
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> CreateEvent()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventRequest request)
        {
            var response = await _eventService.CreateEvent(request);
            if (response.IsSuccessful)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("Create", "Ticket", new { eventId = response.Value.Id }); 
            }
            TempData["ErrorMessage"] = response.Message;
            return View(request);
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var _event = await _eventService.GetEventAsync(id);
            if (_event.IsSuccessful)
            {
                TempData["SuccessMessage"] = _event.Message;
                return View(_event.Value);
            }
            TempData["ErrorMessage"] = _event.Message;
            return RedirectToAction("GetAllEvents");
        }

        [Authorize(Roles = RoleConstant.Admin)]
        public async Task<IActionResult> GetAllEvents()
        {
            var response = await _eventService.GetAllEventsAsync();

            if (response.IsSuccessful)
            {
                var events = response.Value; 
                return View(events);
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("AdminDashboard", "Authentication");
            }
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> UpdateEvent(Guid id)
        {
            var _event = await _eventService.GetEventAsync(id);
            if (_event.IsSuccessful)
            {
                var updateModel = new UpdateEventRequest
                {
                    DateAndTime = _event.Value.DateAndTime ?? DateTime.Now,
                    Description = _event.Value.Description,
                    Duration = _event.Value.Duration ?? TimeSpan.Zero,
                    Title = _event.Value.Title,
                    Type = (EventType)_event.Value.Type,
                    Venue = _event.Value.Venue
                };
                return View(updateModel);
            }
            return RedirectToAction("GetEventsByEventOrganizerId");
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(Guid id, UpdateEventRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var _event = await _eventService.UpdateEventAsync(id, request);
            if (_event.IsSuccessful)
            {
                await _eventService.NotifyAttendeesOnEventUpdateAsync(id); 
                TempData["SuccessMessage"] = _event.Message;
                return RedirectToAction("GetEventsByEventOrganizerId");
            }

            TempData["ErrorMessage"] = _event.Message;
            return View(request);
        }


        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var _event = await _eventService.GetEventAsync(id);
            if (_event.IsSuccessful)
            {
                return View(_event.Value);
            }
            return RedirectToAction("EventOrganizerDashBoard", "Authentication");
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        [HttpPost, ActionName("DeleteEvent")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var _event = await _eventService.GetEventAsync(id);
            if (!_event.IsSuccessful)
            {
                TempData["ErrorMessage"] = _event.Message;
                return RedirectToAction("Index", "Home");
            }

            var attendeesResult = await _eventService.GetAttendeessByEventIdAsync(id);
            if (!attendeesResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = attendeesResult.Message;
                var eventRemovalResult = await _eventService.RemoveEventAsync(id);
                if (eventRemovalResult.IsSuccessful)
                {
                    TempData["SuccessMessage"] = eventRemovalResult.Message;
                    return RedirectToAction("GetEventsByEventOrganizerId");
                }
                else
                {
                    TempData["ErrorMessage"] = eventRemovalResult.Message;
                    return RedirectToAction("Index", "Home");
                }
            }

            var attendees = attendeesResult.Value;

            foreach (var attendee in attendees)
            {
                var walletResponse = await _walletService.GetWalletByUserIdAsync(attendee.UserId);
                if (!walletResponse.IsSuccessful)
                {
                    TempData["ErrorMessage"] = walletResponse.Message;
                    return RedirectToAction("Index", "Home");
                }

                var walletId = walletResponse.Value.Id;

                var purchasedTickets = attendee.TicketRecords.Where(t => t.EventId == _event.Value.Id).ToList();

                foreach (var ticket in purchasedTickets)
                {
                    //decimal totalRefundAmount = ticket.TicketTypeCounts.Sum(ttc => ttc.Price * ttc.Count);

                    decimal totalRefundAmount = ticket.TotalPrice;
                    decimal adminAmount = totalRefundAmount * 0.30M;
                    decimal organizerAmount = totalRefundAmount * 0.70M;

                    // Deduct from Admin's Wallet
                    var adminDeductionResult = await _walletService.DeductFromAdminWalletAsync(adminAmount);
                    if (!adminDeductionResult.IsSuccessful)
                    {
                        TempData["ErrorMessage"] = adminDeductionResult.Message;
                        return RedirectToAction("Index", "Home");
                    }

                    // Deduct from Event Organizer's Wallet
                    var organizerDeductionResult = await _walletService.DeductFromOrganizerWalletAsync(_event.Value.EventOrganizerId, organizerAmount);
                    if (!organizerDeductionResult.IsSuccessful)
                    {
                        TempData["ErrorMessage"] = organizerDeductionResult.Message;
                        return RedirectToAction("Index", "Home");
                    }

                    // Refund to Attendee's Wallet
                    var refundResult = await _walletService.FundWalletAsync(walletId, totalRefundAmount);
                    if (!refundResult.IsSuccessful)
                    {
                        TempData["ErrorMessage"] = refundResult.Message;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // Send Cancellation Emails
            var emailResult = await _eventService.SendCancellationEmailsAsync(id, attendees);
            if (!emailResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = emailResult.Message;
                return RedirectToAction("Index", "Home");
            }

            var eventRemovalResults = await _eventService.RemoveEventAsync(id);
            if (eventRemovalResults.IsSuccessful)
            {
                TempData["SuccessMessage"] = eventRemovalResults.Message;
                return RedirectToAction("GetEventsByEventOrganizerId");
            }
            else
            {
                TempData["ErrorMessage"] = eventRemovalResults.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> GetEventsByEventOrganizerId(Guid organizerId)
        {
            var eventOrganizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId();
            var events = await _eventService.GetEventsByEventOrganizerId(eventOrganizerId);
            if(events.Value != null)
            {
                return View(events.Value);
            }
            TempData["EventErrorMessage"] = events.Message;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventss(string searchQuery)
        {
            var events = await _eventService.GetAllEventsAsync(searchQuery);

            ViewBag.IsAdmin = User.IsInRole(RoleConstant.Admin);

            return View(events);
        }
    }
}
