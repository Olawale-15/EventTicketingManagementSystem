using EventTicketingApp.Constant;
using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Infrastructure.Context;
using EventTicketingApp.Infrastructure.Repositories;
using EventTicketingApp.Models;
using EventTicketingApp.Models.AttendeeModel;
using EventTicketingApp.Models.EventModell;
using EventTicketingApp.Models.EventOrganizerModel;
using System.Security.Claims;

namespace EventTicketingApp.Core.Application.Services
{
    public class EventOrganizerService : IEventOrganizerService
    {
        private readonly IEventOrganizerRepository _eventOrganizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileRepository _fileRepository;
        private readonly EventTicketingAppContext _EventTicketingAppContext;
        private readonly IWalletRepository _walletRepository;
        private readonly IHttpContextAccessor _httpContext;

        public EventOrganizerService(IEventOrganizerRepository eventOrganizerRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IFileRepository fileRepository, EventTicketingAppContext EventTicketingAppContext, IWalletRepository walletRepository, IHttpContextAccessor httpContext)
        {
            _eventOrganizerRepository = eventOrganizerRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
            _EventTicketingAppContext = EventTicketingAppContext;
            _walletRepository = walletRepository;
            _httpContext = httpContext;
        }

        public async Task<BaseResponse> CreateEventOrganizer(CreateEventOrganizerRequest request)
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
            var eventOrganizerRoleId = _EventTicketingAppContext.Roles.FirstOrDefault(r => r.Name == RoleConstant.EventOrganizer).Id;
            var eventOrganizerRole = _EventTicketingAppContext.Roles.FirstOrDefault(r => r.Name == RoleConstant.EventOrganizer);

            var newUser = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = hashedPassword,
                Salt = salt,
                RoleId = eventOrganizerRoleId,
                Role = eventOrganizerRole
            };

            await _userRepository.AddAsync(newUser);
            await _unitOfWork.SaveAsync();

            var eventOrganizer = new EventOrganizer
            {
                Address = request.Address,
                FullName = request.FullName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                UserId = newUser.Id,
                User = newUser,
                CertificationImage = await _fileRepository.UploadAsync(request.CertificationImage),
                EventsCreated = new List<Event>()
            };

            await _eventOrganizerRepository.AddAsync(eventOrganizer);
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

            return new BaseResponse
            {
                Message = "Registration Successfull!",
                IsSuccessful = true,
            };
        }

        public async Task<BaseResponse<ICollection<EventOrganizerResponse>>> GetAllEventOrganizers()
        {
            var eventOrganizers = await _eventOrganizerRepository.GetAllAsync();
            return new BaseResponse<ICollection<EventOrganizerResponse>>
            {
                Message = "List Of EventOrganizer",
                IsSuccessful = true,
                Value = eventOrganizers.Select(x => new EventOrganizerResponse
                {
                    FullName = x.FullName,
                    Address = x.Address,
                    Password = x.User.PasswordHash,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.User.Email,
                    Id = x.Id,
                    CertificationImage = x.CertificationImage,
                    UserName = x.User.UserName,
                    Events = x.EventsCreated.Select(e => new EventResponse
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Description = e.Description,
                        DateAndTime = e.DateAndTime,
                        Venue = e.Venue,
                        Status = e.Status,
                        Type = e.Type
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<BaseResponse<EventOrganizerResponse>> GetEventOrganizer(Guid id)
        {
            var eventOrganizer = await _eventOrganizerRepository.GetAsync(id);
            if (eventOrganizer == null)
            {
                return new BaseResponse<EventOrganizerResponse>
                {
                    Message = "EventOrganizer Not Found!",
                    IsSuccessful = false
                };
            }

            var events = eventOrganizer.EventsCreated.Select(e => new EventResponse
            {
                Id = e.Id,
                Title = e.Title,
                DateAndTime = e.DateAndTime,
                Venue = e.Venue,
                Category = e.Category,
                Status = e.Status,
            }).ToList();

            return new BaseResponse<EventOrganizerResponse>
            {
                Message = "EventOrganizer Found Successfully",
                IsSuccessful = true,
                Value = new EventOrganizerResponse
                {
                    Id = eventOrganizer.Id,
                    FullName = eventOrganizer.FullName,
                    Email = eventOrganizer.User.Email,
                    Address = eventOrganizer.Address,
                    PhoneNumber = eventOrganizer.PhoneNumber,
                    CertificationImage = eventOrganizer.CertificationImage,
                    UserName = eventOrganizer.User.UserName,
                    Events = events
                }
            };
        }

        public async Task<BaseResponse<EventOrganizerResponse>> GetEventOrganizerByUserId(Guid id)
        {
            var eventOrganizers = await _eventOrganizerRepository.GetAllAsync();
            var eventOrganizer = eventOrganizers.FirstOrDefault(x => x.UserId == id);
            if (eventOrganizer == null)
            {
                return new BaseResponse<EventOrganizerResponse>
                {
                    Message = "EventOrganizer Not Found",
                    IsSuccessful = false
                };
            }

            return new BaseResponse<EventOrganizerResponse>
            {
                Message = "EventOrganizer Found Successfully",
                IsSuccessful = true,
                Value = new EventOrganizerResponse
                {
                    Id = eventOrganizer.Id,
                    FullName = eventOrganizer.FullName,
                    Email = eventOrganizer.User.Email,
                    Password = eventOrganizer.User.PasswordHash,
                    Address = eventOrganizer.Address,
                    PhoneNumber = eventOrganizer.PhoneNumber,
                    UserName = eventOrganizer.User.UserName,
                    CertificationImage = eventOrganizer.CertificationImage
                }
            };
        }

        public async Task<BaseResponse> RemoveEventOrganizer(Guid id)
        {
            var eventOrganizer = await _eventOrganizerRepository.GetAsync(id);
            if (eventOrganizer == null)
            {
                return new BaseResponse
                {
                    Message = "Event Organizer Not Found!",
                    IsSuccessful = false
                };
            }

            // Check if the organizer has any ongoing or upcoming events
            var ongoingOrUpcomingEvents = eventOrganizer.EventsCreated
                .Where(e => e.DateAndTime > DateTime.UtcNow || (e.DateAndTime <= DateTime.UtcNow && e.EndDate >= DateTime.UtcNow))
                .ToList();

            if (ongoingOrUpcomingEvents.Any())
            {
                return new BaseResponse
                {
                    Message = "Cannot delete profile: There are ongoing or upcoming events created by this organizer.",
                    IsSuccessful = false
                };
            }

            var user = await _userRepository.GetAsync(eventOrganizer.UserId);

            await _userRepository.RemoveAsync(user);
            await _eventOrganizerRepository.RemoveAsync(eventOrganizer);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "Event Organizer successfully deleted.",
                IsSuccessful = true
            };
        }

        public async Task<BaseResponse> UpdateEventOrganizer(Guid id, UpdateEventOrganizerRequest request)
        {
            var eventOrganizer = await _eventOrganizerRepository.GetAsync(id);
            if (eventOrganizer == null)
            {
                return new BaseResponse
                {
                    Message = "EventOrganizer Not Found!",
                    IsSuccessful = false
                };
            }

            var user = await _userRepository.GetAsync(eventOrganizer.UserId);

            eventOrganizer.FullName = request.FullName ?? eventOrganizer.FullName;
            eventOrganizer.Address = request.Address ?? eventOrganizer.Address;
            eventOrganizer.PhoneNumber = request.PhoneNumber ?? eventOrganizer.PhoneNumber;
            eventOrganizer.User.Email = request.Email ?? eventOrganizer.User.Email;
            eventOrganizer.User.UserName = request.UserName ?? eventOrganizer.User.UserName;

            user.Email = request.Email ?? user.Email;
            user.UserName = request.UserName ?? user.UserName;

            await _userRepository.UpdateAsync(user);
            await _eventOrganizerRepository.UpdateAsync(eventOrganizer);
            await _unitOfWork.SaveAsync();

            return new BaseResponse
            {
                Message = "EventOrganizer Updated Successfully",
                IsSuccessful = true
            };
        }

        public async Task<Guid> GetLoggedInEventOrganizerId()
        {
            var loggedInUserEmail = _httpContext.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var eventOrganizer = await _eventOrganizerRepository.GetAsync(x => x.User.Email == loggedInUserEmail);
            return eventOrganizer.Id;
        }
    }
}
