using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Moq;
using Xunit;

namespace EduHub.Tests;

public class EventServiceTests
{
    [Fact]
    public void CreateEvent_WithValidInput_ShouldCallAddAndSendExactlyOnce()
    {
        var mockRepo = new Mock<IEventRepository>();
        var mockNotification = new Mock<INotificationService>();
        var service = new EventService(mockRepo.Object, mockNotification.Object);

        service.CreateEvent(Guid.NewGuid(), "Tech Workshop", DateTime.UtcNow.AddDays(5), 100, "email");

        mockRepo.Verify(r => r.Add(It.IsAny<Event>()), Times.Once);
        mockNotification.Verify(n => n.Send(It.IsAny<string>()), Times.Once);
    }
}
