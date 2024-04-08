using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatusPageLibrary.Services;

namespace StatusPageLibrary;

public static class Helpers
{
    /// <summary>
    /// A helper method to add the StatusPageLibrary to the IServiceCollection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown if an <see cref="IConfiguration"/> has not been added to the <paramref name="services"/></exception>
    public static IServiceCollection AddStatusPageLibrary(this IServiceCollection services)
    {
        // ReSharper disable once SimplifyLinqExpressionUseAll
        if(!services.Any(descriptor => descriptor.ServiceType == typeof(IConfiguration)))
        {
            throw new InvalidOperationException(
                "IConfiguration is required to use the StatusPageLibrary. Please add it to the service collection.");
        }
        
        services.AddHttpClient();
        services.AddSingleton<IHttpClientService, HttpClientService>();
        services.AddSingleton<IIncidentsService, IncidentsService>();
        return services;
    }
}