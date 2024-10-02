using Kinetique.Appointment.API.Services;
using Kinetique.Appointment.Application;
using Kinetique.Appointment.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDAL(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddHostedService<AppointmentRabbitService>()
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
