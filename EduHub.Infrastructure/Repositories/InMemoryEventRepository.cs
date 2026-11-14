using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;

namespace EduHub.Infrastructure.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = new();

    public void Add(Event evt) => _events.Add(evt);
    public IEnumerable<Event> GetByClubId(Guid clubId) => _events.Where(e => e.ClubId == clubId).ToList();
    public Event? GetById(Guid id) => _events.FirstOrDefault(e => e.Id == id);
    public bool Delete(Guid id)
    {
        var evt = GetById(id);
        return evt != null && _events.Remove(evt);
    }
}
