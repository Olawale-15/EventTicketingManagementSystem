using EventTicketingApp.Core.Application.Interfaces.Repositories;
using EventTicketingApp.Core.Application.Interfaces.Services;
using EventTicketingApp.Core.Application.Services;
using EventTicketingApp.Infrastructure.Context;
using EventTicketingApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace EventTicketingApp.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<EventTicketingAppContext>(a => a.UseMySQL(connectionString));

        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IAttendeeRepository, AttendeeRepository>()
                .AddScoped<IFileRepository, FileRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IWalletRepository, WalletRepository>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<IEventRepository, EventRepository>()
                .AddScoped<ITicketRepository, TicketRepository>()
                .AddScoped<IEventOrganizerRepository, EventOrganizerRepository>()
                .AddScoped<IAttendeeTicketRecordRepository, AttendeeTicketRecordRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IAttendeeService, AttendeeService>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IWalletService, WalletService>()
                .AddScoped<ITransactionService, TransactionService>()
                .AddScoped<IEventService, EventService>()
                .AddScoped<ITicketService, TicketService>()
                .AddScoped<IEventOrganizerService, EventOrganizerService>()
                .AddScoped<IMailServices, EmailSender>()
                .AddScoped<IAttendeeTicketRecordService, AttendeeTicketRecordService>();
        }
    }

}
