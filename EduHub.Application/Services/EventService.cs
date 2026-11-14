using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;

namespace EduHub.Application.Services;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly INotificationService? _notificationService;
    private readonly INotificationFactory? _notificationFactory;

    public EventService(IEventRepository eventRepository, INotificationService notificationService)
    {
        _eventRepository = eventRepository;
        _notificationService = notificationService;
    }

    public EventService(IEventRepository eventRepository, INotificationFactory notificationFactory)
    {
        _eventRepository = eventRepository;
        _notificationFactory = notificationFactory;
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel)
    {
        var evt = Event.Create(clubId, title, date, capacity);
        _eventRepository.Add(evt);

        var notifier = _notificationFactory != null
            ? _notificationFactory.Create(channel)
            : _notificationService!;

        notifier.Send($"New event: {title}");
        return evt;
    }

    public IEnumerable<Event> GetEventsByClub(Guid clubId) => _eventRepository.GetByClubId(clubId);
    public Event? GetEventById(Guid id) => _eventRepository.GetById(id);
    public bool DeleteEvent(Guid id) => _eventRepository.Delete(id);
}
