using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Data;

public class HelpDeskDbContext : DbContext
{
    public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = 1,
                Title = "Email not syncing on mobile",
                Description = "Outlook app on Android stops syncing after 10 minutes. Reinstall did not help.",
                Priority = "High",
                Status = "Open",
                RaisedBy = "manas.tiwar@company.com",
                CreatedDate = new DateTime(2026, 8, 4, 9, 15, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                Id = 2,
                Title = "Request for MacBook charger",
                Description = "Need a replacement USB-C charger for the engineering team laptop.",
                Priority = "Medium",
                Status = "In Progress",
                RaisedBy = "dev.team@company.com",
                CreatedDate = new DateTime(2026, 8, 3, 11, 0, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                Id = 3,
                Title = "Printer offline in Block B",
                Description = "HP LaserJet on 2nd floor shows offline status for all users.",
                Priority = "Low",
                Status = "Closed",
                RaisedBy = "hr@company.com",
                CreatedDate = new DateTime(2026, 8, 1, 14, 30, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                Id = 4,
                Title = "New employee onboarding access",
                Description = "Set up Active Directory, Slack, and GitHub access for new joiner starting Monday.",
                Priority = "High",
                Status = "Open",
                RaisedBy = "manas.tiwar@company.com",
                CreatedDate = new DateTime(2026, 8, 5, 8, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
