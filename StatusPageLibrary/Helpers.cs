using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatusPageLibrary.Services;

namespace StatusPageLibrary;

public static class Helpers
{
    /// <summary>
    /// A helper method to add the StatusPageLibrary to an <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">The services collection where the library will be used.</param>
    /// <param name="configuration">A <see cref="IncidentsService.Configuration"/> that contains the necessary values.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown if an <see cref="IConfiguration"/> has not been added to the <paramref name="services"/></exception>
    public static IServiceCollection AddStatusPageLibrary(
        this IServiceCollection services,
        IncidentsService.Configuration configuration)
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
        services.AddSingleton(_ => configuration);
        return services;
    }
}