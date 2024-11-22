using Kinetique.Main.Application;
using Kinetique.Main.DAL;
using Kinetique.Shared;
using Kinetique.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// var provider = builder.Configuration.GetValue("Provider", "Postgres");

builder.Services.AddDAL(builder.Configuration)
    .AddApplication()
    .AddRabbitMqRpc(builder.Configuration)
    .AddSwaggerGen()
    .AddShared()
    .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();
await using var scope = app.Services.CreateAsyncScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
await dbContext.Database.MigrateAsync();

app.UseShared();
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