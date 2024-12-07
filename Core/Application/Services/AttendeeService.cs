using EventTicketingApp.Constant;
using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Core.Domain.Enums;
using EventTicketingApp.Infrastructure.Context;
using EventTicketingApp.Infrastructure.Repositories;
using EventTicketingApp.Models;
using EventTicketingApp.Models.AttendeeModel;
using EventTicketingApp.Models.AttendeeTicketRecord;
using EventTicketingApp.Models.TicketModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventTicketingApp.Core.Application.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private readonly EventTicketingAppContext _EventTicketingAppContext;
        private readonly IWalletRepository _walletRepository;
        private readonly IAttendeeTicketRecordRepository _attendeeTicketRecordRepository;

        public AttendeeService(IAttendeeRepository attendeeRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext, EventTicketingAppContext EventTicketingAppContext, IWalletRepository walletRepository, IAttendeeTicketRecordRepository attendeeTicketRecordRepository)
        {
            _attendeeRepository = attendeeRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
            _EventTicketingAppContext = EventTicketingAppContext;
            _walletRepository = walletRepository;
            _attendeeTicketRecordRepository = attendeeTicketRecordRepository;
        }

        public async Task<BaseResponse> CreateAttendee(CreateAttendeeRequest request)
        {
            var exists = await _userRepository.ExistsAsync(request.Email);
            if (exists)
            {
                return new BaseResponse
                {
                    Message = "Email Already Exists!",
                    IsSuccessful = false
                };
            }

            var user = await _userRepository.GetAsync(u => u.Email == request.Email);
            if (user?.PasswordHash != null && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) || user?.UserName == request.UserName)
            {
                return new BaseResponse
                {
                    Message = "Password Or UserName Already Exists!",
                    IsSuccessful = false
                };
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            var attendeeRoleId = _EventTicketingAppContext.Roles.FirstOrDefault(r => r.Name == RoleConstant.Attendee).Id;
            var attendeeRole = _EventTicketingAppContext.Roles.FirstOrDefault(r => r.Name == RoleConstant.Attendee);

            var newUser = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = hashedPassword,
                Salt = salt,
                RoleId = attendeeRoleId,
                Role = attendeeRole
            };

            await _userRepository.AddAsync(newUser);
            await _unitOfWork.SaveAsync();

            var attendee = new Attendee
            {
                Address = request.Address,
                FullName = request.FullName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                UserId = newUser.Id,
                User = newUser,
                Tickets = new List<Ticket>()
            };

            await _attendeeRepository.AddAsync(attendee);
            await _unitOfWork.SaveAsync();

            var wallet = new Wallet
            {
                Balance = 0m,
                User = newUser,
                UserId = newUser.Id,
                Transactions = new List<Transaction>()
            };

            await _walletRepository.AddAsync(wallet);
            await _unitOfWork.SaveAsync();

            newUser.Wallet = wallet;
            await _userRepository.UpdateAsync(newUser);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Registration Successfull!",
                IsSuccessful = true,
            };
        }

        //public async Task<BaseResponse<ICollection<AttendeeResponse>>> GetAllAttendees()
        //{
        //    var attendees = await _attendeeRepository.GetAllAsync();

        //    var attendeeResponses = attendees.Select(x => new AttendeeResponse
        //    {
        //        Id = x.Id,
        //        UserId = x.User.Id,
        //        Email = x.User.Email,
        //        Password = x.User.PasswordHash,
        //        UserName = x.User.UserName,
        //        FullName = x.FullName,
        //        PhoneNumber = x.PhoneNumber,
        //        Address = x.Address,

        //        Tickets = x.Tickets.Select(t => new TicketResponse
        //        {
        //            Id = t.Id,
        //            Type = t.Type,
        //            EventId = t.Event.Id,
        //            EventName = t.Event.Title,
        //            AttendeeId = x.Id,
        //            QRCode = t.QRCode,
        //            VerificationCode = t.VerificationCode,
        //            TicketTypeCounts = new List<TicketTypeCount>
        //        {
        //        new TicketTypeCount
        //        {
        //            Type = t.Type,
        //            Count = t.Count,
        //            Price = t.Price
        //        }
        //    }
        //        }).ToList()
        //    }).ToList();

        //    return new BaseResponse<ICollection<AttendeeResponse>>
        //    {
        //        Message = "List Of Attendees",
        //        IsSuccessful = true,
        //        Value = attendeeResponses
        //    };
        //}

        public async Task<BaseResponse<ICollection<AttendeeResponse>>> GetAllAttendees()
        {
            var attendees = await _attendeeRepository.GetAllAsync();
            if (attendees == null || !attendees.Any())
            {
                return new BaseResponse<ICollection<AttendeeResponse>>
                {
                    IsSuccessful = false,
                    Message = "No attendees found."
                };
            }

            var attendeeResponses = new List<AttendeeResponse>();

            foreach (var attendee in attendees)
            {
                // Fetch attendee's ticket records
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

                // Map to AttendeeResponse
                var attendeeResponse = new AttendeeResponse
                {
                    Id = attendee.Id,
                    UserId = attendee.User.Id,
                    Email = attendee.User.Email,
                    Password = attendee.User.PasswordHash,
                    UserName = attendee.User.UserName,
                    FullName = attendee.FullName,
                    PhoneNumber = attendee.PhoneNumber,
                    Address = attendee.Address,
                    Tickets = attendee.Tickets.Select(t => new TicketResponse
                    {
                        Id = t.Id,
                        Type = t.Type,
                        EventId = t.Event.Id,
                        EventName = t.Event.Title,
                        AttendeeId = attendee.Id,
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
                Message = "List Of Attendees",
                IsSuccessful = true,
                Value = attendeeResponses
            };
        }


        public async Task<BaseResponse<AttendeeResponse>> GetAttendee(Guid id)
        {
            var attendee = await _attendeeRepository.GetAsync(id);
            if (attendee == null)
            {
                return new BaseResponse<AttendeeResponse>
                {
                    Message = "Attendee Not Found!",
                    IsSuccessful = false
                };
            }
          
            return new BaseResponse<AttendeeResponse>
            {
                Message = "Attendee Found Successfully",
                IsSuccessful = true,
                Value = new AttendeeResponse
                {
                    Id = attendee.Id,
                    FullName = attendee.FullName,
                    Email = attendee.User.Email,
                    Password = attendee.User.PasswordHash,
                    Address = attendee.Address,
                    PhoneNumber = attendee.PhoneNumber,
                    UserName = attendee.User.UserName
                }
            };
        }

        public async Task<BaseResponse<AttendeeResponse>> GetAttendeeByUserId(Guid id)
        {
            var attendees = await _attendeeRepository.GetAllAsync();
            var attendee = attendees.FirstOrDefault(x => x.UserId == id);
            if (attendee == null)
            {
                return new BaseResponse<AttendeeResponse>
                {
                    Message = "Attendee Not Found",
                    IsSuccessful = false
                };
            }

            return new BaseResponse<AttendeeResponse>
            {
                Message = "Attendee Found Successfully",
                IsSuccessful = true,
                Value = new AttendeeResponse
                {
                    Id = attendee.Id,
                    FullName = attendee.FullName,
                    Email = attendee.User.Email,
                    Password = attendee.User.PasswordHash,
                    Address = attendee.Address,
                    PhoneNumber = attendee.PhoneNumber,
                    UserName = attendee.User.UserName
                }
            };
        }

        public async Task<BaseResponse> RemoveAttendee(Guid id)
        {
            var attendee = await _attendeeRepository.GetAsync(id);
            if (attendee == null)
            {
                return new BaseResponse
                {
                    Message = "Attendee Not Found!",
                    IsSuccessful = false
                };
            }

            var user = await _userRepository.GetAsync(attendee.UserId);

            await _userRepository.RemoveAsync(user);
            await _attendeeRepository.RemoveAsync(attendee);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Attendee Successfully Deleted",
                IsSuccessful = true
            };
        }

        public async Task<BaseResponse> UpdateAttendee(Guid id, UpdateAttendeeRequest request)
        {
            var attendee = await _attendeeRepository.GetAsync(id);
            if (attendee == null)
            {
                return new BaseResponse
                {
                    Message = "Attendee Not Found!",
                    IsSuccessful = false
                };
            }

            var user = await _userRepository.GetAsync(attendee.UserId);

            attendee.FullName = request.FullName ?? attendee.FullName;
            attendee.Address = request.Address ?? attendee.Address;
            attendee.PhoneNumber = request.PhoneNumber ?? attendee.PhoneNumber;
            attendee.User.Email = request.Email ?? attendee.User.Email;
            attendee.User.UserName = request.UserName ?? attendee.User.UserName;

            user.Email = request.Email ?? user.Email;
            user.UserName = request.UserName ?? user.UserName;

            await _userRepository.UpdateAsync(user);
            await _attendeeRepository.UpdateAsync(attendee);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Attendee Updated Successfully",
                IsSuccessful = true
            };
        }

        public async Task<Guid> GetLoggedInAttendeeId()
        {
            var loggedInUserEmail = _httpContext.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var attendee = await _attendeeRepository.GetAsync(x => x.User.Email == loggedInUserEmail);
            return attendee.Id;
        }
    }
}
