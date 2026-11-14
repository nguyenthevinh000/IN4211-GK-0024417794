using EduHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

public record CreateEventRequest(string Title, DateTime Date, int Capacity, string Channel = "email");

[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("api/clubs/{clubId:guid}/events")]
    public IActionResult GetEventsByClub(Guid clubId) => Ok(_eventService.GetEventsByClub(clubId));

    [HttpGet("api/events/{id:guid}")]
    public IActionResult GetEventById(Guid id)
    {
        var evt = _eventService.GetEventById(id);
        return evt == null ? NotFound(new { message = "Event not found." }) : Ok(evt);
    }

    [HttpPost("api/clubs/{clubId:guid}/events")]
    public IActionResult CreateEvent(Guid clubId, [FromBody] CreateEventRequest request)
    {
        try
        {
            var evt = _eventService.CreateEvent(clubId, request.Title, request.Date, request.Capacity, request.Channel);
            return CreatedAtAction(nameof(GetEventById), new { id = evt.Id }, evt);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("api/events/{id:guid}")]
    public IActionResult DeleteEvent(Guid id) =>
        _eventService.DeleteEvent(id) ? NoContent() : NotFound(new { message = "Event not found." });
}
