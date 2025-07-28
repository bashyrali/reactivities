using API.Middleware;
using Application.Activities;
using Application.Core;
using Application.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddCors(opt => opt.AddPolicy("Cors",
            policy => { policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000"); }));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddAutoMapper(typeof(MapProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
        //services.AddTransient<ExceptionMiddleware>();
        return services;
    }
}