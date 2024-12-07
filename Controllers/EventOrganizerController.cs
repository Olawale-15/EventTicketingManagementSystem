using EventTicketingApp.Constant;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Models.EventOrganizerModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingApp.Controllers
{
    public class EventOrganizerController : Controller
    {
        private readonly IEventOrganizerService _eventOrganizerService;
        public EventOrganizerController(IEventOrganizerService eventOrganizerService)
        {
            _eventOrganizerService = eventOrganizerService;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterEventOrganizer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEventOrganizer(CreateEventOrganizerRequest request)
        {
            var response = await _eventOrganizerService.CreateEventOrganizer(request);
            if (!response.IsSuccessful)
            {
                TempData["ErrorMessage"] = response.Message;
                return View(request);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Login", "Authentication");
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> GetEventOrganizer()
        {
            var organizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId();
            var eventOrganizer = await _eventOrganizerService.GetEventOrganizer(organizerId);
            if (eventOrganizer.IsSuccessful)
            {
                TempData["SuccessMessage"] = eventOrganizer.Message;
                return View(eventOrganizer.Value);
            }
            TempData["ErrorMessage"] = eventOrganizer.Message;
            return RedirectToAction("Login", "Authentication");
        }

        [Authorize(Roles = RoleConstant.Admin)]
        public async Task<IActionResult> GetAllEventOrganizers()
        {
            var response = await _eventOrganizerService.GetAllEventOrganizers();
            if (response.IsSuccessful)
            {
                var organizers = response.Value; 
                return View(organizers);
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("AdminDashboard");
            }
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> UpdateEventOrganizer()
        {
            var organizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId();
            var eventOrganizer = await _eventOrganizerService.GetEventOrganizer(organizerId);
            if (eventOrganizer.IsSuccessful)
            {
                var updateModel = new UpdateEventOrganizerRequest
                {
                    Address = eventOrganizer.Value.Address,
                    Email = eventOrganizer.Value.Email,
                    FullName = eventOrganizer.Value.FullName,
                    PhoneNumber = eventOrganizer.Value.PhoneNumber,
                    UserName = eventOrganizer.Value.UserName
                };
                return View(updateModel);
            }
            return RedirectToAction("GetEventOrganizer");
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        [HttpPost]
        public async Task<IActionResult> UpdateEventOrganizer(UpdateEventOrganizerRequest request)
        {
            var organizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId();
            var eventOrganizer = await _eventOrganizerService.UpdateEventOrganizer(organizerId, request);
            if (eventOrganizer.IsSuccessful)
            {
                TempData["SuccessMessage"] = eventOrganizer.Message;
                return RedirectToAction("GetEventOrganizer");
            }
            TempData["ErrorMessage"] = eventOrganizer.Message;
            var updateModel = new UpdateEventOrganizerRequest
            {
                FullName = request.FullName,
                Address = request.Address,
                PhoneNumber = request.Address,
                UserName = request.UserName,
                Email = request.Email
            };
            return View(updateModel);
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> DeleteEventOrganizer()
        {
            var organizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId();
            var eventOrganizer = await _eventOrganizerService.GetEventOrganizer(organizerId);
            if (eventOrganizer.IsSuccessful)
            {
                return View(eventOrganizer.Value);
            }
            return RedirectToAction("EventOrganizerDashBoard", "Authentication");
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        [HttpPost, ActionName("DeleteEventOrganizer")]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var organizerId = await _eventOrganizerService.GetLoggedInEventOrganizerId();
            var eventOrganizer = await _eventOrganizerService.RemoveEventOrganizer(organizerId);
            if (eventOrganizer.IsSuccessful)
            {
                TempData["SuccessMessage"] = eventOrganizer.Message;
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = eventOrganizer.Message;
            return RedirectToAction("EventOrganizerDashBoard", "Authentication");
        }
    }
}
