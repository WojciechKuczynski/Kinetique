using Kinetique.Main.API.Services;
using Kinetique.Main.Application;
using Kinetique.Main.DAL;
using Kinetique.Shared;
using Kinetique.Shared.Filters;
using Kinetique.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// var provider = builder.Configuration.GetValue("Provider", "Postgres");

builder.Services.AddDAL(builder.Configuration)
    .CreateRabbitTopology(builder.Configuration)
    .AddApplication()
    .AddHostedService<PatientDetailsRabbitService>(x => new PatientDetailsRabbitService(x,builder.Configuration))
    .AddRabbitMqRpc(builder.Configuration)
    .AddSwaggerGen()
    .AddShared()
    .AddControllers(options =>
    {
        options.Filters.Add<LogActionFilter>();
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    });

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
Console.WriteLine("Version 1.0");
// app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(e => e.MapControllers());
app.Run();
