using LinkShortener.Contracts;
using LinkShortener.Repository;
using LinkShortener.Service.Contracts;
using LinkShortener.Service;
using Microsoft.EntityFrameworkCore;
namespace LinkShortener.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("mysqlConnection");
        services.AddDbContext<RepositoryContext>(options =>
           options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        using var serviceScope = services.BuildServiceProvider().CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<RepositoryContext>();
        dbContext.Database.Migrate();
    }
}