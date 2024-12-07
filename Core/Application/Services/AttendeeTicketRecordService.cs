using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Domain.Entities;
using EventTicketingApp.Models.AttendeeTicketRecord;
using EventTicketingApp.Models;
using EventTicketingApp.Core.Domain.Enums;

namespace EventTicketingApp.Core.Application.Services
{
    public class AttendeeTicketRecordService : IAttendeeTicketRecordService
    {
        private readonly IAttendeeTicketRecordRepository _attendeeTicketRecordRepository;

        public AttendeeTicketRecordService(IAttendeeTicketRecordRepository attendeeTicketRecordRepository)
        {
            _attendeeTicketRecordRepository = attendeeTicketRecordRepository;
        }

        public async Task<BaseResponse<AttendeeTicketRecordResponse>> GetByIdAsync(Guid id)
        {
            var record = await _attendeeTicketRecordRepository.GetByIdAsync(id);
            if (record == null)
            {
                return new BaseResponse<AttendeeTicketRecordResponse>
                {
                    IsSuccessful = false,
                    Message = "Record not found"
                };
            }

            return new BaseResponse<AttendeeTicketRecordResponse>
            {
                IsSuccessful = true,
                Value = MapToResponse(record)
            };
        }

        public async Task<BaseResponse<IEnumerable<AttendeeTicketRecordResponse>>> GetByAttendeeIdAsync(Guid attendeeId)
        {
            var records = await _attendeeTicketRecordRepository.GetByAttendeeIdAsync(attendeeId);

            return new BaseResponse<IEnumerable<AttendeeTicketRecordResponse>>
            {
                IsSuccessful = true,
                Value = records.Select(MapToResponse).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<AttendeeTicketRecordResponse>>> GetByEventIdAsync(Guid eventId)
        {
            var records = await _attendeeTicketRecordRepository.GetByEventIdAsync(eventId);

            return new BaseResponse<IEnumerable<AttendeeTicketRecordResponse>>
            {
                IsSuccessful = true,
                Value = records.Select(MapToResponse).ToList()
            };
        }

        public async Task<BaseResponse<AttendeeTicketRecordResponse>> AddAsync(AttendeeTicketRecord record)
        {
            var newRecord = await _attendeeTicketRecordRepository.AddAsync(record);

            return new BaseResponse<AttendeeTicketRecordResponse>
            {
                IsSuccessful = true,
                Value = MapToResponse(newRecord)
            };
        }

        public async Task<BaseResponse<AttendeeTicketRecordResponse>> UpdateAsync(AttendeeTicketRecord record)
        {
            var updatedRecord = await _attendeeTicketRecordRepository.UpdateAsync(record);

            return new BaseResponse<AttendeeTicketRecordResponse>
            {
                IsSuccessful = true,
                Value = MapToResponse(updatedRecord)
            };
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var record = await _attendeeTicketRecordRepository.GetByIdAsync(id);
            await _attendeeTicketRecordRepository.DeleteAsync(record);

            return new BaseResponse
            {
                IsSuccessful = true,
                Message = "Record deleted successfully"
            };
        }

        private AttendeeTicketRecordResponse MapToResponse(AttendeeTicketRecord record)
        {
            return new AttendeeTicketRecordResponse
            {
                Id = record.Id,
                AttendeeId = record.AttendeeId,
                AttendeeName = record.Attendee.FullName,
                EventId = record.EventId,
                EventTitle = record.Event.Title,
                Type = record.Ticket.Type,
                Count = record.TicketCount,
                TotalPrice = record.TotalPrice
            };
        }
    }
}
