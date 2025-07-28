using System.Reflection;
using API.Extensions;
using API.Middleware;
using Application.Activities;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddIdentityExtensions(builder.Configuration);


// builder.Host 

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DataContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        context.Database.Migrate();
        await Seed.SeedData(context, userManager);
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occured during migration");
        Console.WriteLine(e);
    }
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("Cors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();