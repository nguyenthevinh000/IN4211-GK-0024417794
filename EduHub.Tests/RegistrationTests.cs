using EduHub.Domain.Entities;
using Xunit;

namespace EduHub.Tests;

public class RegistrationTests
{
    [Fact]
    public void Create_WithValidIds_ShouldHavePendingStatus()
    {
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        Assert.Equal(RegistrationStatus.Pending, r.Status);
    }

    [Fact]
    public void Create_WithEmptyIds_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, Guid.NewGuid()));
    }

    [Fact]
    public void Confirm_FromPending_ShouldChangeStatusToConfirmed()
    {
        var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
        r.Confirm();
        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Confirm_AfterCancel_ShouldThrowInvalidOperationException_AndRemainCancelled()
    {
        var memberId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var r = Registration.Create(memberId, eventId);
        r.Cancel();
        Assert.Throws<InvalidOperationException>(() => r.Confirm());
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }
}
