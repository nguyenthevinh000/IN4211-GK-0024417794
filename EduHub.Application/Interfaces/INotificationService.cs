namespace EduHub.Application.Interfaces;

public interface INotificationService
{
    void Send(string message);
}

public interface INotificationFactory
{
    INotificationService Create(string channel);
}
