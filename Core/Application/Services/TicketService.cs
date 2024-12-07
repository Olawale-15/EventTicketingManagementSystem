using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Infrastructure.Repositories;
using EventTicketingApp.Models;
using EventTicketingApp.Models.TicketModel;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace EventTicketingApp.Core.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMailServices _emailSender;
        private readonly IEventOrganizerRepository _eventOrganizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttendeeTicketRecordRepository _attendeeTicketRecordRepository;
        public TicketService(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, IAttendeeRepository attendeeRepository, IEventRepository eventRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository, IMailServices emailSender, IEventOrganizerRepository eventOrganizerRepository, IUserRepository userRepository, IAttendeeTicketRecordRepository attendeeTicketRecordRepository)
        {
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
            _attendeeRepository = attendeeRepository;
            _eventRepository = eventRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _emailSender = emailSender;
            _eventOrganizerRepository = eventOrganizerRepository;
            _userRepository = userRepository;
            _attendeeTicketRecordRepository = attendeeTicketRecordRepository;
        }

        public async Task<BaseResponse> CreateTicketAsync(CreateTicketRequest request)
        {
            var _event = await _eventRepository.GetAsync(request.EventId);
            if (_event == null)
            {
                return new BaseResponse
                {
                    Message = "Event Not Found",
                    IsSuccessful = false
                };
            }

            var existingTicket = _event.Tickets.FirstOrDefault(t => t.Type == request.Type);

            if (existingTicket != null)
            {
                existingTicket.Count += request.Count;

                if (request.Price > 0)
                {
                    existingTicket.Price = request.Price;
                }

                await _ticketRepository.UpdateAsync(existingTicket);
            }
            else
            {
                var newTicket = new Ticket
                {
                    Event = _event,
                    EventId = _event.Id,
                    Price = request.Price,
                    Type = request.Type,
                    Count = request.Count,
                };

                await _ticketRepository.AddAsync(newTicket);
            }

            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = existingTicket != null ? "Ticket count updated successfully." : "New ticket type created successfully.",
                IsSuccessful = true
            };
        }


        public async Task<BaseResponse<ICollection<TicketResponse>>> GetTicketsByTypeAsync(Guid eventId, TicketType type)
        {
            var _event = await _eventRepository.GetAsync(eventId);
            if(_event == null)
            {
                return new BaseResponse<ICollection<TicketResponse>>
                {
                    Message = "Event Not Found",
                    IsSuccessful = false
                };
            }

            var tickets = await _ticketRepository.GetAllAsync();
            var filteredTickets = tickets.Where(t => t.EventId == eventId && t.Type == type).ToList();

            if (filteredTickets.Count == 0)
            {
                return new BaseResponse<ICollection<TicketResponse>>
                {
                    Message = "No Tickets Found For This Type",
                    IsSuccessful = false
                };
            }

            var ticketResponses = filteredTickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                Type = t.Type,
                QRCode = t.QRCode,
                VerificationCode = t.VerificationCode,
                AttendeeId = (Guid)t.AttendeeId,
                TicketTypeCounts = new List<TicketTypeCount>
                {
                    new TicketTypeCount
                    {
                        Type = t.Type,
                        Count = t.Count,
                        Price = t.Price
                    }
                }
            }).ToList();

            return new BaseResponse<ICollection<TicketResponse>>
            {
                Message = "List Of Tickets",
                IsSuccessful = true,
                Value = ticketResponses
            };
        }

        public async Task<BaseResponse> UpdateTicketAsync(UpdateTicketRequest request)
        {
            var ticket = await _ticketRepository.GetAsync(request.TicketId);
            if (ticket == null)
            {
                return new BaseResponse
                {
                    Message = "Ticket Not Found",
                    IsSuccessful = false
                };
            }

            ticket.Price = request.Price;
            ticket.Count = request.Count;
            ticket.Type = request.Type;

            await _ticketRepository.UpdateAsync(ticket);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Ticket Updated Successfully",
                IsSuccessful = true
            };
        }

        public async Task<BaseResponse<ICollection<TicketResponse>>> GetTicketsByAttendeeIdAsync(Guid attendeeId)
        {
            var attendee = await _attendeeRepository.GetAsync(attendeeId);
            if (attendee == null)
            {
                return new BaseResponse<ICollection<TicketResponse>>
                {
                    Message = "Attendee Not Found!",
                    IsSuccessful = false
                };
            }

            var tickets = attendee.Tickets;

            var ticketResponses = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                Type = t.Type,
                QRCode = t.QRCode,
                VerificationCode = t.VerificationCode,
                AttendeeId = (Guid)t.AttendeeId,
                EventName = t.Event.Title,
                TicketTypeCounts = new List<TicketTypeCount>
                {
                    new TicketTypeCount
                    {
                        Type = t.Type,
                        Count = t.Count,
                        Price = t.Price
                    }
                }
            }).ToList();

            return new BaseResponse<ICollection<TicketResponse>>
            {
                Message = "List Of Tickets",
                IsSuccessful = true,
                Value = ticketResponses
            };
        }

        public async Task<BaseResponse<ICollection<TicketResponse>>> GetTicketsByEventIdAsync(Guid eventId)
        {
            var _event = await _eventRepository.GetAsync(eventId);
            if (_event == null)
            {
                return new BaseResponse<ICollection<TicketResponse>>
                {
                    Message = "Event Not Found",
                    IsSuccessful = false
                };
            }

            var tickets = _event.Tickets;

            var ticketResponses = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                Type = t.Type,
                QRCode = t.QRCode,
                VerificationCode = t.VerificationCode,
                AttendeeId = (Guid)(t.AttendeeId.HasValue ? t.AttendeeId.Value : (Guid?)null),
                TicketTypeCounts = new List<TicketTypeCount>
                {
                    new TicketTypeCount
                    {
                        Type = t.Type,
                        Count = t.Count,
                        Price = t.Price
                    }
                }
            }).ToList();

            return new BaseResponse<ICollection<TicketResponse>>
            {
                Message = "List Of Tickets",
                IsSuccessful = true,
                Value = ticketResponses
            };
        }

        public async Task<BaseResponse<ICollection<TicketResponse>>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return new BaseResponse<ICollection<TicketResponse>>
            {
                Message = "List Of Tickets",
                IsSuccessful = true,
                Value = tickets.Select(x => new TicketResponse
                {
                    AttendeeId = (Guid)x.AttendeeId,
                    EventId = x.EventId,
                    Id = x.Id,
                    QRCode = x.QRCode,
                    Type = x.Type,
                    VerificationCode = x.VerificationCode,
                    TicketTypeCounts = new List<TicketTypeCount>
                    {
                        new TicketTypeCount
                        {
                            Type = x.Type,
                            Count = x.Count,
                            Price = x.Price
                        }
                    }
                }).ToList()
            };
        }


        public async Task<BaseResponse> BuyTicketsAsync(Guid attendeeId, List<BuyTicketRequest> ticketPurchases)
        {
            var attendee = await _attendeeRepository.GetAsync(attendeeId);
            if (attendee == null)
            {
                return new BaseResponse
                {
                    IsSuccessful = false,
                    Message = "Attendee not found."
                };
            }

            decimal totalAmount = 0;
            var eventTicketUpdates = new Dictionary<Guid, List<(TicketType Type, int Count)>>();
            foreach (var purchase in ticketPurchases)
            {
                var eventDetails = await _eventRepository.GetAsync(purchase.EventId);
                if (eventDetails == null)
                {
                    return new BaseResponse
                    {
                        IsSuccessful = false,
                        Message = $"Event with ID {purchase.EventId} not found."
                    };
                }

                var eventTicket = eventDetails.Tickets.FirstOrDefault(t => t.Type == purchase.Type);
                if (eventTicket == null || eventTicket.Count < purchase.Count)
                {
                    return new BaseResponse
                    {
                        IsSuccessful = false,
                        Message = $"Insufficient tickets for type {purchase.Type}."
                    };
                }

                totalAmount += eventTicket.Price * purchase.Count;
                if (!eventTicketUpdates.ContainsKey(eventDetails.Id))
                {
                    eventTicketUpdates[eventDetails.Id] = new List<(TicketType Type, int Count)>();
                }
                eventTicketUpdates[eventDetails.Id].Add((purchase.Type, purchase.Count));
            }

            var attendeeWallet = await _walletRepository.GetAsync(a => a.UserId == attendee.UserId);
            if (attendeeWallet == null || attendeeWallet.Balance < totalAmount)
            {
                return new BaseResponse
                {
                    IsSuccessful = false,
                    Message = "Insufficient funds."
                };
            }

            foreach (var purchase in ticketPurchases)
            {
                var eventDetails = await _eventRepository.GetAsync(purchase.EventId);
                var eventTicket = eventDetails.Tickets.First(t => t.Type == purchase.Type);

                eventTicket.AttendeeId = attendee.Id;
                eventTicket.Attendee = attendee;

                var ticketRecord = new AttendeeTicketRecord
                {
                    Attendee = attendee,
                    AttendeeId = attendee.Id,
                    EventId = eventDetails.Id,
                    TicketCount = purchase.Count,
                    TotalPrice = eventTicket.Price * purchase.Count,
                    TicketId = eventTicket.Id
                };

                attendee.TicketRecords.Add(ticketRecord);
                attendee.Tickets.Add(eventTicket);
                await _attendeeTicketRecordRepository.AddAsync(ticketRecord);
                await _ticketRepository.UpdateAsync(eventTicket);
                await _unitOfWork.SaveAsync();
            }

            var eventId = ticketPurchases.First().EventId;
            var _event = await _eventRepository.GetAsync(eventId);
            var userId = _event.Organizer.UserId;
            var user = await _userRepository.GetAsync(userId);
            var eventOrganizerWallet = user.Wallet;

            var adminWallet = await _walletRepository.GetAdminWalletAsync();

            decimal adminPercentage = 0.30m;
            decimal organizerPercentage = 0.70m;

            decimal adminAmount = totalAmount * adminPercentage;
            decimal organizerAmount = totalAmount * organizerPercentage;

            attendeeWallet.Balance -= totalAmount;
            adminWallet.Balance += adminAmount;
            eventOrganizerWallet.Balance += organizerAmount;

            await _walletRepository.UpdateAsync(attendeeWallet);
            await _walletRepository.UpdateAsync(eventOrganizerWallet);
            await _walletRepository.UpdateAsync(adminWallet);

            foreach (var eventUpdate in eventTicketUpdates)
            {
                var eventDetails = await _eventRepository.GetAsync(eventUpdate.Key);
                foreach (var ticketUpdate in eventUpdate.Value)
                {
                    var eventTicket = eventDetails.Tickets.First(t => t.Type == ticketUpdate.Type);
                    eventTicket.Count -= ticketUpdate.Count;
                }
                await _eventRepository.UpdateAsync(eventDetails);
            }

            var transaction = new Transaction
            {
                Amount = totalAmount,
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Credit,
                Wallet = attendeeWallet,
                WalletId = attendeeWallet.Id,
            };
            //await _transactionRepository.AddAsync(transaction);

            foreach (var purchase in ticketPurchases)
            {
                var eventDetails = await _eventRepository.GetAsync(purchase.EventId);

                // Check if the ticket count for the specified type is greater than zero
                if (purchase.Count > 0)
                {
                    string emailContent = $"THANK YOU FOR PURCHASING {purchase.Count} {purchase.Type} TICKET(s) FOR {eventDetails.Title}.";

                    if (eventDetails.Type == EventType.Online)
                    {
                        var _tickets = await _ticketRepository.GetAllAsync();
                        var existingTicket = _tickets
                            .FirstOrDefault(t => t.EventId == purchase.EventId && t.VerificationCode != null);

                        string uniqueNumber;

                        if (existingTicket != null)
                        {
                            uniqueNumber = existingTicket.VerificationCode;
                        }
                        else
                        {
                            uniqueNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

                            var ticketToUpdate = _tickets
                                .FirstOrDefault(t => t.EventId == purchase.EventId && t.VerificationCode == null);

                            if (ticketToUpdate != null)
                            {
                                ticketToUpdate.VerificationCode = uniqueNumber;
                                await _ticketRepository.UpdateAsync(ticketToUpdate);
                            }
                        }
                        
                        emailContent += $" THE EVENT PASSCODE IS: {uniqueNumber}";
                        var emailDto = new EmailDto
                        {
                            ToEmail = attendee.User.Email,
                            ToName = attendee.FullName,
                            Subject = "YOUR ONLINE EVENT TICKET",
                            HtmlContent = emailContent
                        };
                        _emailSender.SendEMail(emailDto);
                    }
                    else
                    {
                        string qrCodePath = CreateQrCode(eventDetails, attendee);
                        emailContent += " PLEASE FIND YOUR QR CODE ATTACHED.";
                        var emailDto = new EmailDto
                        {
                            ToEmail = attendee.User.Email,
                            ToName = attendee.FullName,
                            Subject = "YOUR OFFLINE EVENT TICKET",
                            HtmlContent = emailContent
                        };
                        _emailSender.QRCodeEMail(emailDto, qrCodePath);
                    }
                }
            }

            await _unitOfWork.SaveAsync();
            return new BaseResponse
            {
                IsSuccessful = true,
                Message = "Tickets purchased successfully."
            };
        }

        private static string CreateQrCode(Event eventDetails, Attendee attendee)
        {
            string content = $"Event: {eventDetails.Title}\nDate: {eventDetails.DateAndTime}\nAttendee: {attendee.FullName}\nTicket ID: {Guid.NewGuid()}";
            var qrWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions { Height = 100, Width = 100, Margin = 0 }
            };

            var pixelData = qrWriter.Write(content);

            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                string directoryPath = @"C:\Users\LAWALI\source\repos\EventTicketingApp\wwwroot\qrcodepath";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string fileName = $"{Guid.NewGuid()}.png";
                string filePath = Path.Combine(directoryPath, fileName);

                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                return filePath;
            }
        }


    }
}