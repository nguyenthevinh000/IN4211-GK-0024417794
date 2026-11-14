using EduHub.Domain.Entities;

namespace EduHub.Application.Interfaces;

public interface IEventRepository
{
    void Add(Event evt);
    IEnumerable<Event> GetByClubId(Guid clubId);
    Event? GetById(Guid id);
    bool Delete(Guid id);
}
