using Cab.Core.Interface;
using Cab.Infrastructure.Data;
using Cab.Infrastructure.Helpers;
using Cab.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Cab.Infrastructure.Interfaces;
using Cab.Infrastructure.Database;
var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetSection("Cab:ConnectionStrings")["CabDatabase"];

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not configured.");
}

builder.Services.Configure<CabAppSettings>(builder.Configuration.GetSection("Cab"));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserManagementService, UserManagementService>();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

// Register SqlHelper as a singleton
builder.Services.AddSingleton(new SqlHelper(connectionString));

// Register IUserService and its implementation UserService
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

// Add controllers

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

ConfigurationManager configuration = builder.Configuration;
var config = configuration.GetSection("Cab");
//adding config object so that it can be injected

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
            builder => builder.AllowAnyOrigin() // React's default URL
                               .AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod());
});

//JWT Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = config["Jwt:Audience"],
        ValidIssuer = config["Jwt:Issuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30)

    };
});
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Enable serving static files (for React build)
app.UseStaticFiles();

// Enable routing
app.UseRouting();



app.UseCors("AllowReactApp");


app.UseAuthorization();
app.MapControllers();


app.MapFallbackToFile("index.html");
// Run the application
app.Run();

