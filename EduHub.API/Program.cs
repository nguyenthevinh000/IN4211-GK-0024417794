using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddScoped<INotificationFactory, NotificationFactory>();
builder.Services.AddScoped<EventService>(sp =>
    new EventService(
        sp.GetRequiredService<IEventRepository>(),
        sp.GetRequiredService<INotificationFactory>()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
