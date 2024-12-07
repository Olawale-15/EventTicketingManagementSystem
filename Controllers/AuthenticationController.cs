using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Models.AttendeeModel;
using EventTicketingApp.Models.EventOrganizerModel;
using EventTicketingApp.Models.UserModell;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EventTicketingApp.Infrastructure.Context;
using EventTicketingApp.Constant;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAttendeeService _attendeeService;
        private readonly IEventOrganizerService _eventOrganizerService;
        private readonly EventTicketingAppContext _eventTicketingAppContext;

        public AuthenticationController(IUserService userService, IAttendeeService attendeeService, IEventOrganizerService eventOrganizerService, EventTicketingAppContext eventTicketingAppContext)
        {
            _userService = userService;
            _attendeeService = attendeeService;
            _eventOrganizerService = eventOrganizerService;
            _eventTicketingAppContext = eventTicketingAppContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminDashBoard()
        {
            return View();
        }

        public async Task<IActionResult> EventOrganizerDashBoard()
        {
            return View();
        }

        public async Task<IActionResult> AttendeeDashBoard()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            if (ModelState.IsValid)
            {
                var login = await _userService.LoginAsync(request);
                if (!login.IsSuccessful)
                {
                    TempData["ErrorMessage"] = login.Message;
                    return View(request);
                }

                // Retrieve role name based on role ID
                var roleName = await _eventTicketingAppContext.Roles
                    .Where(r => r.Id == login.Value.RoleId)
                    .Select(r => r.Name)
                    .FirstOrDefaultAsync();

                if (roleName == null)
                {
                    TempData["ErrorMessage"] = "User role not found.";
                    return View(request);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, login.Value.Id.ToString()),
                    new Claim(ClaimTypes.Email, login.Value.Email),
                    new Claim(ClaimTypes.Name, login.Value.UserName),
                    new Claim(ClaimTypes.Role, roleName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                switch (roleName)
                {
                    case RoleConstant.Admin:
                        return RedirectToAction("AdminDashBoard");
                    case RoleConstant.EventOrganizer:
                        return RedirectToAction("EventOrganizerDashBoard");
                    case RoleConstant.Attendee:
                        return RedirectToAction("AttendeeDashBoard");
                    default:
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(request);
                }
            }
            return View(request);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}