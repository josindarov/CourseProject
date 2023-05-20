using System.Reflection;
using Application.UseCases.Comment;
using Application.UseCases.Review;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDependencies
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddMaps(typeof(ReviewProfile).Assembly));
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}