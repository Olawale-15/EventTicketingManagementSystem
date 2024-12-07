using EventTicketingApp.Constant;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Models.AttendeeModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventTicketingApp.Controllers
{
    public class AttendeeController : Controller
    {
        private readonly IAttendeeService _attendeeService;
        public AttendeeController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterAttendee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAttendee(CreateAttendeeRequest request)
        {
            var response = await _attendeeService.CreateAttendee(request);
            if (!response.IsSuccessful)
            {
                TempData["ErrorMessage"] = response.Message;
                return View(request);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Login", "Authentication");
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> GetAttendee()
        {
            var attendeeId = await _attendeeService.GetLoggedInAttendeeId();
            var attendee = await _attendeeService.GetAttendee(attendeeId);
            if (attendee.IsSuccessful)
            {
                TempData["SuccessMessage"] = attendee.Message;
                return View(attendee.Value);
            }
            TempData["ErrorMessage"] = attendee.Message;
            return RedirectToAction("AttendeeDashBoard");
        }

        [Authorize(Roles = RoleConstant.Admin)]
        public async Task<IActionResult> GetAllAttendees()
        {
            var response = await _attendeeService.GetAllAttendees();

            if (response.IsSuccessful)
            {
                var attendees = response.Value; 
                return View(attendees);
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("AdminDashboard");
            }
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> UpdateAttendee()
        {
            var attendeeId = await _attendeeService.GetLoggedInAttendeeId();
            var attendee = await _attendeeService.GetAttendee(attendeeId);
            if(attendee.IsSuccessful)
            {
                var updateModel = new UpdateAttendeeRequest
                {
                    Address = attendee.Value.Address,
                    Email = attendee.Value.Email,
                    FullName = attendee.Value.FullName,
                    PhoneNumber = attendee.Value.PhoneNumber,
                    UserName = attendee.Value.UserName
                };
                return View(updateModel);
            }
            return RedirectToAction("GetAttendee");
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        [HttpPost]
        public async Task<IActionResult> UpdateAttendee(UpdateAttendeeRequest request)
        {
            var attendeeId = await _attendeeService.GetLoggedInAttendeeId();
            var attendee = await _attendeeService.UpdateAttendee(attendeeId, request);
            if (attendee.IsSuccessful)
            {
                TempData["SuccessMessage"] = attendee.Message;
                return RedirectToAction("GetAttendee");
            }
            TempData["ErrorMessage"] = attendee.Message;
            var updateModel = new UpdateAttendeeRequest
            {
                FullName = request.FullName,
                Address = request.Address,
                PhoneNumber = request.Address,
                UserName = request.UserName,
                Email = request.Email
            };
            return View(updateModel);
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> DeleteAttendee()
        {
            var attendeeId = await _attendeeService.GetLoggedInAttendeeId();
            var attendee = await _attendeeService.GetAttendee(attendeeId);
            if (attendee.IsSuccessful)
            {
                return View(attendee.Value);
            }
            return RedirectToAction("AttendeeDashboard", "Authentication");
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        [HttpPost, ActionName("DeleteAttendee")]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var attendeeId = await _attendeeService.GetLoggedInAttendeeId();
            var attendee = await _attendeeService.RemoveAttendee(attendeeId);
            if (attendee.IsSuccessful)
            {
                TempData["SuccessMessage"] = attendee.Message;
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = attendee.Message;
            return RedirectToAction("AttendeeDashboard", "Authentication");
        }

    }
}
