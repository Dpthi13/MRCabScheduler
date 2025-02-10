using Cab.Infrastructure.Data;
using Cab.Core.Interface;
using Cab.Service;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string from configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Database connection string is missing.");

// Register SqlHelper as a singleton
builder.Services.AddSingleton(new SqlHelper(connectionString));

// Register IUserService and its implementation UserService
builder.Services.AddScoped<IUserService, UserService>();

// Add controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CabBookingApp API",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
            builder => builder.WithOrigins("http://localhost:3000") // React's default URL
                               .AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Enable serving static files (for React build)
app.UseStaticFiles();

app.UseCors("AllowReactApp");

// Enable routing
app.UseRouting();


app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();

