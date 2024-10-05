using Kinetique.Nfz.API.Services;
using Kinetique.Nfz.Application;
using Kinetique.Nfz.DAL;
using Kinetique.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRabbitMq(builder.Configuration)
    .AddDAL(builder.Configuration)
    .AddApplication()
    .AddHostedService<AppointmentEndRabbitService>()
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

app.UseRouting();
app.UseEndpoints(e => e.MapControllers());
app.Run();