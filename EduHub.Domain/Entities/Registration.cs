namespace EduHub.Domain.Entities;

public class Registration : BaseEntity
{
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public RegistrationStatus Status { get; private set; }

    private Registration(Guid memberId, Guid eventId)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = RegistrationStatus.Pending;
    }

    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty || eventId == Guid.Empty)
        {
            throw new ArgumentException("MemberId and EventId cannot be empty.");
        }

        return new Registration(memberId, eventId);
    }

    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
        {
            throw new InvalidOperationException("Only Pending registration can be confirmed.");
        }

        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
        {
            throw new InvalidOperationException("Registration is already cancelled.");
        }

        Status = RegistrationStatus.Cancelled;
    }
}
