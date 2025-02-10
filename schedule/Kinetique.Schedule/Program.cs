using Kinetique.Schedule;
using Kinetique.Schedule.BackgroundServices;
using Kinetique.Schedule.DAL;
using Kinetique.Shared;
using Kinetique.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddSwaggerGen()
    .AddShared()
    .AddRabbitMq(builder.Configuration)
    .AddHostedService<DoctorScheduleRabbitService>(x => new DoctorScheduleRabbitService(x, builder.Configuration))
    .AddHostedService<AppointmentCreatedSubscriber>()
    .AddHostedService<AppointmentRemovedSubscriber>()
    .AddDAL(builder.Configuration)
    .AddSchedule(builder.Configuration)
    .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();
await using var scope = app.Services.CreateAsyncScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
await dbContext.Database.MigrateAsync();

app.UseShared();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseRouting();
app.UseEndpoints(e => e.MapControllers());

app.Run();