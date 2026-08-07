using HelpDesk.Api.Controllers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelpDesk.Tests;

public class TicketControllerTests
{
    private readonly Mock<ITicketRepository> _mockRepo;
    private readonly TicketController _controller;

    public TicketControllerTests()
    {
        _mockRepo = new Mock<ITicketRepository>();
        _controller = new TicketController(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllTickets_ReturnsOkResult_WhenTicketExist()
    {
        // Arrange
        var tickets = new List<Ticket>
        {
            new Ticket { Id = 1, Title = "Issue 1", Description = "Desc 1", Priority = "High", Status = "Open", RaisedBy = "user1@test.com" },
            new Ticket { Id = 2, Title = "Issue 2", Description = "Desc 2", Priority = "Low", Status = "Closed", RaisedBy = "user2@test.com" }
        };
        _mockRepo.Setup(r => r.GetAllTicketsAsync()).ReturnsAsync(tickets);

        // Act
        var result = await _controller.GetAllTickets();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
        Assert.Equal(2, returnTickets.Count);
    }

    [Fact]
    public async Task GetTicketById_ReturnsOkResult_WhenTicketExists()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Title = "Issue 1", Description = "Desc 1", Priority = "High", Status = "Open", RaisedBy = "user1@test.com" };
        _mockRepo.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(ticket);

        // Act
        var result = await _controller.GetTicketById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTicket = Assert.IsType<Ticket>(okResult.Value);
        Assert.Equal(1, returnTicket.Id);
    }

    [Fact]
    public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetTicketByIdAsync(99)).ReturnsAsync((Ticket?)null);

        // Act
        var result = await _controller.GetTicketById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully()
    {
        // Arrange
        var ticket = new Ticket { Title = "New Issue", Description = "Desc", Priority = "Medium", Status = "Open", RaisedBy = "user@test.com" };
        _mockRepo.Setup(r => r.CreateTicketAsync(ticket)).ReturnsAsync(10);

        // Act
        var result = await _controller.CreateTicket(ticket);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(10, okResult.Value);
    }

    [Fact]
    public async Task CreateTicket_ReturnsBadRequest_WhenTicketIsNull()
    {
        // Act
        var result = await _controller.CreateTicket(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist()
    {
        // Arrange
        var openTickets = new List<Ticket>
        {
            new Ticket { Id = 1, Title = "Open Issue", Description = "Desc", Priority = "High", Status = "Open", RaisedBy = "user@test.com" }
        };
        _mockRepo.Setup(r => r.GetTicketsByStatusAsync("Open")).ReturnsAsync(openTickets);

        // Act
        var result = await _controller.GetTicketsByStatus("Open");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
        Assert.Single(returnTickets);
        Assert.Equal("Open", returnTickets[0].Status);
    }

    [Fact]
    public async Task UpdateTicket_ReturnsOkResult_WhenUpdateIsSuccessful()
    {
        // Arrange
        var existingTicket = new Ticket { Id = 1, Title = "Old Title", Description = "Desc", Priority = "Low", Status = "Open", RaisedBy = "user@test.com" };
        var updatedTicket = new Ticket { Id = 1, Title = "Updated Title", Description = "Updated Desc", Priority = "High", Status = "In Progress", RaisedBy = "user@test.com" };

        _mockRepo.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(existingTicket);
        _mockRepo.Setup(r => r.UpdateTicketAsync(updatedTicket)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateTicket(1, updatedTicket);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTicket = Assert.IsType<Ticket>(okResult.Value);
        Assert.Equal("Updated Title", returnTicket.Title);
    }

    [Fact]
    public async Task UpdateTicket_ReturnsNotFound_WhenTicketDoesNotExist()
    {
        // Arrange
        var ticket = new Ticket { Id = 99, Title = "Non-existent", Description = "Desc", Priority = "Low", Status = "Open", RaisedBy = "user@test.com" };
        _mockRepo.Setup(r => r.GetTicketByIdAsync(99)).ReturnsAsync((Ticket?)null);

        // Act
        var result = await _controller.UpdateTicket(99, ticket);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteTicket_ReturnsOkResult_WhenTicketIsDeletedSuccessfully()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Title = "To Delete", Description = "Desc", Priority = "Low", Status = "Closed", RaisedBy = "user@test.com" };
        _mockRepo.Setup(r => r.GetTicketByIdAsync(1)).ReturnsAsync(ticket);
        _mockRepo.Setup(r => r.DeleteTicketAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteTicket(1);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteTicket_ReturnsNotFound_WhenTicketDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetTicketByIdAsync(99)).ReturnsAsync((Ticket?)null);

        // Act
        var result = await _controller.DeleteTicket(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAllTickets_ReturnsEmptyList_WhenNoTicketsExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllTicketsAsync()).ReturnsAsync(new List<Ticket>());

        // Act
        var result = await _controller.GetAllTickets();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
        Assert.Empty(returnTickets);
    }

    [Fact]
    public async Task GetTicketsByStatus_ReturnsEmptyList_WhenNoMatchingTicketsExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetTicketsByStatusAsync("Closed")).ReturnsAsync(new List<Ticket>());

        // Act
        var result = await _controller.GetTicketsByStatus("Closed");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
        Assert.Empty(returnTickets);
    }
}
