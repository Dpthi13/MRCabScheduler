using Cab.Infrastructure.Database;
using Cab.Infrastructure.Helpers;
using Cab.Infrastructure.Interfaces;
using Cab.Service;
using Cab.Web.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetSection("Cab:ConnectionStrings")["CabDatabase"];

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not configured.");
}

builder.Services.Configure<CabAppSettings>(builder.Configuration.GetSection("Cab"));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});


// Register
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
builder.Services.AddSingleton<IUserManagementService, UserManagementService>();
builder.Services.AddSingleton<IRequestCabService, RequestCabService>();
builder.Services.AddSingleton<ICabDataService, CabDataService>();

//adding config object so that it can be injected

ConfigurationManager configuration = builder.Configuration;
var config = configuration.GetSection("Cab");

//adding cors
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

//builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI();
app.UseHttpsRedirection();
var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
app.UseRouting();

// Enable serving static files (for React build)
app.UseStaticFiles();

// Enable routing
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/wwwroot"
});

app.UseCors("AllowReactApp");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.MapFallbackToFile("index.html");
// Run the application
app.Run();

