using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Notifications;

public class EmailNotificationService : INotificationService
{
    public void Send(string message) => Console.WriteLine($"[EMAIL] {message}");
}

public class SmsNotificationService : INotificationService
{
    public void Send(string message) => Console.WriteLine($"[SMS] {message}");
}

public class NotificationFactory : INotificationFactory
{
    public INotificationService Create(string channel) => channel?.ToLowerInvariant() switch
    {
        "sms" => new SmsNotificationService(),
        _ => new EmailNotificationService()
    };
}
