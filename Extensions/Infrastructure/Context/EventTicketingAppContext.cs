using EventTicketingApp.Constant;
using EventTicketingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace EventTicketingApp.Infrastructure.Context
{
    public class EventTicketingAppContext : DbContext
    {
        public EventTicketingAppContext(DbContextOptions<EventTicketingAppContext> options) : base(options)
        {

        }

        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventOrganizer> EventOrganizers => Set<EventOrganizer>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<AttendeeTicketRecord> AttendeeTicketRecords => Set<AttendeeTicketRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminRoleId = Guid.NewGuid();
            var eventOrganizerRoleId = Guid.NewGuid();
            var attendeeRoleId = Guid.NewGuid();

            var adminWalletId = Guid.NewGuid();
            var adminId = Guid.NewGuid();

            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Name = RoleConstant.Admin },
                new Role { Id = eventOrganizerRoleId, Name = RoleConstant.EventOrganizer },
                new Role { Id = attendeeRoleId, Name = RoleConstant.Attendee });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    Email = "admin@gmail.com",
                    Salt = salt,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass", salt),
                    UserName = "CEO",
                    RoleId = adminRoleId
                });

            modelBuilder.Entity<Wallet>().HasData(
                new Wallet
                {
                    Id = adminWalletId,
                    UserId = adminId,
                    Balance = 0m
                });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId);

            modelBuilder.Entity<Attendee>()
                .HasMany(a => a.Tickets)
                .WithOne(t => t.Attendee)
                .HasForeignKey(t => t.AttendeeId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Attendee)
                .WithMany(a => a.Tickets)
                .HasForeignKey(t => t.AttendeeId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete
        



    }
}
}
