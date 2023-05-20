using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDependencies
{
    public static void AddInfrastructureDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

    }
}