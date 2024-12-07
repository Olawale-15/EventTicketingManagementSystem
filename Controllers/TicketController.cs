using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Models.TicketModel;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;
using EventTicketingApp.Models.TransactionModel;
using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Domain.Entities;
using System.Reflection.Metadata.Ecma335;
using EventTicketingApp.Infrastructure.Repositories;
using EventTicketingApp.Models.EventModell;
using System.Security.Claims;
using EventTicketingApp.Models.AttendeeModel;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Constant;
using Microsoft.AspNetCore.Authorization;
using EventTicketingApp.Core.Application.Services;
namespace EventTicketingApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IConfiguration _configuration;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventRepository _eventRepository;
        private readonly IAttendeeService _attendeeService;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IEventService _eventService;
        private readonly IWalletService _walletService;
        private readonly ITransactionService _transactionService;
        private readonly IAttendeeTicketRecordService _attendeeTicketRecordService;
        private readonly string Token;
        private  PayStackApi PayStack { get; set; }
        public TicketController(ITicketService ticketService, IConfiguration configuration, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IEventRepository eventRepository, IAttendeeService attendeeService, IEventService eventService, IWalletService walletService, IAttendeeRepository attendeeRepository, ITransactionService transactionService, IAttendeeTicketRecordService attendeeTicketRecordService)
        {
            _ticketService = ticketService;
            _configuration = configuration;
            Token = _configuration["PayStack:SecretKey"];
            PayStack = new PayStackApi(Token);
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _eventRepository = eventRepository;
            _attendeeService = attendeeService;
            _eventService = eventService;
            _walletService = walletService;
            _attendeeRepository = attendeeRepository;
            _transactionService = transactionService;
            _attendeeTicketRecordService = attendeeTicketRecordService;
        }

        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public IActionResult Create(Guid eventId)
        {
            var model = new CreateTicketRequest { EventId = eventId };
            ViewBag.TicketTypes = new SelectList(Enum.GetValues(typeof(TicketType)));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.EventOrganizer)]
        public async Task<IActionResult> Create(CreateTicketRequest request)
        {
            var response = await _ticketService.CreateTicketAsync(request);
            if (response.IsSuccessful)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("GetEventsByEventOrganizerId", "Event"); 
            }
            TempData["ErrorMessage"] = response.Message;
            ViewBag.TicketTypes = new SelectList(Enum.GetValues(typeof(TicketType)));
            return View(request);
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> Tickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var attendee = await _attendeeService.GetAttendeeByUserId(Guid.Parse(userId));
            if (attendee == null)
            {
                TempData["ErrorMessage"] = "Attendee not found.";
                return RedirectToAction("Index", "Home");
            }

            var response = await _attendeeTicketRecordService.GetByAttendeeIdAsync(attendee.Value.Id);
            if (response.IsSuccessful)
            {
                return View(response.Value);
            }

            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> MyTransactions(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var attendee = await _attendeeService.GetAttendeeByUserId(Guid.Parse(userId));
            if (attendee == null)
            {
                TempData["ErrorMessage"] = "Attendee not found.";
                return RedirectToAction("Index", "Home");
            }

            var response = await _transactionService.GetTransactionsByAttendeeId(attendee.Value.Id);

            if (!response.IsSuccessful)
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("Index", "Home");
            }

            return View(response.Value);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> BuyTicket(Guid eventId)
        {
            var eventDetails = await _eventRepository.GetAsync(eventId);
            if (eventDetails == null)
            {
                return NotFound();
            }

            var ticketTypes = eventDetails.Tickets.Select(t => new TicketTypeViewModel
            {
                Type = t.Type,
                Price = t.Price,
                AvailableCount = t.Count
            }).ToList();

            var model = new TicketPurchaseViewModel
            {
                EventName = eventDetails.Title,
                TicketTypes = ticketTypes
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> BuyTicket(Guid eventId, TicketPurchaseViewModel model)
        {
            var wallet = await _walletService.GetLoggedInUserWalletAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var attendee = await _attendeeService.GetAttendeeByUserId(Guid.Parse(userId));

            if (!attendee.IsSuccessful)
            {
                ViewData["error"] = "Attendee not found.";
                return View(model);
            }

            var attendeeId = attendee.Value.Id;

            var ticketPurchases = model.SelectedTickets.Select(st => new BuyTicketRequest
            {
                EventId = eventId,
                Type = st.Type,
                Count = st.Count
            }).ToList();

            decimal totalAmount = 0;
            foreach (var tp in ticketPurchases)
            {
                var ticketPrice = await _eventRepository.GetTicketPrice(tp.EventId, tp.Type);
                totalAmount += tp.Count * ticketPrice;
            }

            var result = await _ticketService.BuyTicketsAsync(attendeeId, ticketPurchases);
            if (!result.IsSuccessful)
            {
                ViewData["error"] = result.Message;
                return View(model);
            }

            TransactionInitializeRequest request = new()
            {
                AmountInKobo = (int)(totalAmount * 100),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Reference = GenerateReference().ToString(),
                Currency = "NGN",
                CallbackUrl = "https://localhost:7040/ticket/verifypayment"
            };

            TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);
            if (response.Status)
            {
                var transaction = new Transaction()
                {
                    Amount = totalAmount,
                    Email = User.FindFirstValue(ClaimTypes.Email),
                    TransactionReference = request.Reference,
                    Status = false,
                    CreatedAt = DateTime.UtcNow,
                    WalletId = wallet.Value.Id,
                    TransactionDate = DateTime.UtcNow,
                    Type = TransactionType.Debit
                };
                await _transactionRepository.AddAsync(transaction);
                await _unitOfWork.SaveAsync();

                return Redirect(response.Data.AuthorizationUrl);
            }
            ViewData["error"] = response.Message;
            return View(model);
        }

        public async Task<IActionResult> VerifyPayment(string reference)
        {
            TransactionVerifyResponse response = PayStack.Transactions.Verify(reference);

            if (response.Data.Status == "success")
            {
                var transaction = await _transactionRepository.GetAsync(a => a.TransactionReference == reference);

                if (transaction != null)
                {
                    transaction.Status = true;
                    await _transactionRepository.UpdateAsync(transaction);
                    await _unitOfWork.SaveAsync();

                    var ticketTransactions = await _transactionRepository.GetTicketPurchasesForTransaction(reference);

                    // Map Transaction to BuyTicketRequest
                    var ticketPurchases = ticketTransactions
                        .Where(tt => tt.EventId.HasValue && tt.Ticket.HasValue && tt.Count.HasValue)
                        .Select(tt => new BuyTicketRequest
                        {
                            EventId = tt.EventId.Value,  
                            Type = tt.Ticket.Value,      
                            Count = tt.Count.Value      
                        })
                        .ToList();
                    return RedirectToAction("MyTransactions");
                }
            }
            else
            {
                ViewData["error"] = response.Data.GatewayResponse;
            }

            return RedirectToAction("GetAllEvents", "Event");
        }

        public static string GenerateReference()
        {
            return Guid.NewGuid().ToString("N");
        }

    }
}
