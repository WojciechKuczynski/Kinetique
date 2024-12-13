using Kinetique.Shared.Middlewares;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        // services.AddSingleton<ErrorHandlingMiddleware>();
        services.AddScoped<IClock, UtcClock>();
        return services;
    }
    
    public static IApplicationBuilder UseShared(this IApplicationBuilder app)
    {
        // app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }
}