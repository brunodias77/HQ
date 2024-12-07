using System.Reflection;
using FluentMigrator.Runner;
using HQ.Domain.Repositories;
using HQ.Domain.Security.PasswordEncripter;
using HQ.Infra.Data;
using HQ.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HQ.Infra.Configurations;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        AddFluentMigrator(services, configuration);
        AddDbContext(services, configuration);
        AddPasswordEncrypter(services, configuration);
        AddRepositories(services);
    }

    
   
    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddFluentMigratorCore()
            .ConfigureRunner(options =>
            {
                options.AddPostgres() 
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.Load("HQ.Infra")).For.All(); 
            })
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
    
    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter, Security.PasswordEncripter.BCrypt>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUserRepository, UserRepository>();
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        // Configuração para PostgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
}