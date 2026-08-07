using HelpDesk.Api.Data;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly HelpDeskDbContext _context;

    public TicketRepository(HelpDeskDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets.FindAsync(id);
    }

    public async Task<int> CreateTicketAsync(Ticket ticket)
    {
        if (ticket.CreatedDate == default)
        {
            ticket.CreatedDate = DateTime.UtcNow;
        }
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket.Id;
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        var existing = await _context.Tickets.FindAsync(ticket.Id);
        if (existing != null)
        {
            existing.Title = ticket.Title;
            existing.Description = ticket.Description;
            existing.Priority = ticket.Priority;
            existing.Status = ticket.Status;
            existing.RaisedBy = ticket.RaisedBy;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTicketAsync(int id)
    {
        var existing = await _context.Tickets.FindAsync(id);
        if (existing != null)
        {
            _context.Tickets.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)
    {
        return await _context.Tickets
            .Where(t => t.Status.ToLower() == status.ToLower())
            .ToListAsync();
    }
}
