using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _repository;

    public TicketController(ITicketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllTickets()
    {
        var tickets = await _repository.GetAllTicketsAsync();
        return Ok(tickets);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        var ticket = await _repository.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] Ticket? ticket)
    {
        if (ticket == null)
        {
            return BadRequest();
        }
        var createdId = await _repository.CreateTicketAsync(ticket);
        return Ok(createdId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket? ticket)
    {
        if (ticket == null)
        {
            return BadRequest();
        }

        var existing = await _repository.GetTicketByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        ticket.Id = id;
        await _repository.UpdateTicketAsync(ticket);
        return Ok(ticket);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var existing = await _repository.GetTicketByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _repository.DeleteTicketAsync(id);
        return Ok();
    }

    [HttpGet("Status/{status}")]
    public async Task<IActionResult> GetTicketsByStatus(string status)
    {
        var tickets = await _repository.GetTicketsByStatusAsync(status);
        return Ok(tickets);
    }
}
