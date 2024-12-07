using EventTicketingApp.Constant;
using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Models.WalletModell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;
using System.Security.Claims;

namespace EventTicketingApp.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;
        private readonly string Token;

        private PayStackApi PayStack { get; set; }
        public WalletController(IWalletService walletService, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            _walletService = walletService;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            Token = _configuration["PayStack:SecretKey"];
            PayStack = new PayStackApi(Token);
            _httpContext = httpContext;
        }

        public async Task<IActionResult> GetWallet()
        {
            var wallet = await _walletService.GetLoggedInUserWalletAsync();
            if (wallet.IsSuccessful)
            {
                var userRole = User.IsInRole("Admin") ? "Admin" :
                               User.IsInRole("EventOrganizer") ? "EventOrganizer" :
                               "Attendee";

                ViewBag.UserRole = userRole;
                return View(wallet.Value);
            }

            TempData["ErrorMessage"] = wallet.Message;
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> FundWallet(Guid id)
        {
            var myWallet = await _walletService.GetLoggedInUserWalletAsync();
            var wallet = await _walletService.GetWallet(myWallet.Value.Id);
            if (wallet.IsSuccessful)
            {
                var model = new UpdateWalletRequest
                {
                    Balance = wallet.Value.Balance,
                    Id = wallet.Value.Id
                };
                return View(model);
            }
            TempData["ErrorMessage"] = wallet.Message ?? "Failed to load wallet.";
            return RedirectToAction("GetWallet");
        }

        [HttpPost]
        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> FundWallet(Guid id, UpdateWalletRequest request)
        {
            var loggedInUserEmail = _httpContext.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View(request);
            }

            var myWallet = await _walletService.GetLoggedInUserWalletAsync();
            if (!myWallet.IsSuccessful)
            {
                TempData["ErrorMessage"] = "Failed to retrieve wallet.";
                return View(request);
            }

            int amountInKobo = (int)(request.Balance * 100);

            var transactionRequest = new TransactionInitializeRequest
            {
                AmountInKobo = amountInKobo,
                Email = loggedInUserEmail, // Assuming the user's email is the username
                Reference = Guid.NewGuid().ToString(), // Generate a unique reference
                Currency = "NGN",
                CallbackUrl = "https://localhost:7040/wallet/verifypayment"
            };

            TransactionInitializeResponse response = PayStack.Transactions.Initialize(transactionRequest);
            if (response.Status)
            {
                var transaction = new Transaction
                {
                    Amount = request.Balance,
                    Email = loggedInUserEmail,
                    TransactionReference = transactionRequest.Reference,
                    Status = false, 
                    CreatedAt = DateTime.Now,
                    WalletId = myWallet.Value.Id,
                    Type = TransactionType.Credit,
                    TransactionDate = DateTime.UtcNow
                };
                await _transactionRepository.AddAsync(transaction);
                await _unitOfWork.SaveAsync();

                return Redirect(response.Data.AuthorizationUrl);
            }

            TempData["ErrorMessage"] = "Failed to initialize payment. Please try again.";
            return View(request);
        }

        [Authorize(Roles = RoleConstant.Attendee)]
        public async Task<IActionResult> VerifyPayment(string reference)
        {
            var myWallet = await _walletService.GetLoggedInUserWalletAsync();

            var verifyResponse = PayStack.Transactions.Verify(reference);

            if (verifyResponse.Data.Status == "success")
            {
                var transaction = await _transactionRepository.GetAsync(a => a.TransactionReference == reference);
                if (transaction != null)
                {
                    transaction.Status = true;
                    await _transactionRepository.UpdateAsync(transaction);
                    await _unitOfWork.SaveAsync();

                    var walletResponse = await _walletService.FundWalletAsync(myWallet.Value.Id, transaction.Amount);
                    if (walletResponse.IsSuccessful)
                    {
                        TempData["SuccessMessage"] = "Wallet funded successfully!";
                        return RedirectToAction("GetWallet");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Payment verified but failed to update wallet balance. Please contact support.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Transaction not found or already completed.";
                }
            }
            else
            {
                ViewData["error"] = verifyResponse.Data.GatewayResponse;
            }

            return RedirectToAction("GetWallet");
        }

    }
}
