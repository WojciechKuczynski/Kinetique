using Kinetique.Appointment.API.Services;
using Kinetique.Appointment.Application;
using Kinetique.Appointment.DAL;
using Kinetique.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDAL(builder.Configuration)
    .AddHostedService<AppointmentRabbitService>()
    .AddApplication(builder.Configuration)
    .AddSwaggerGen()
    .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();
await using var scope = app.Services.CreateAsyncScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
await dbContext.Database.MigrateAsync();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(e => e.MapControllers());
app.Run();
