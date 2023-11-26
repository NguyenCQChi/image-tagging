using System.Text;
using Asp.Versioning;
using authentication;
using authentication.Data;
using authentication.Filters;
using authentication.Middleware;
using authentication.Models;
using authentication.Repository;
using authentication.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Configure Logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("log/log.txt", rollingInterval:RollingInterval.Day).CreateLogger();

// Add services to the container.
builder.Host.UseSerilog();
var aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
if (string.Compare(aspNetCoreEnvironment, "Development", StringComparison.Ordinal) == 0)
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                foreach (var origin in builder.Configuration.GetSection("ApiSettings:Audience").Get<List<string>>()!)
                {
                    policy.WithOrigins(origin);
                }
            });
    });
}

builder.Services.AddControllers(option => { option.ReturnHttpNotAcceptable = true; }).AddNewtonsoftJson();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddResponseCaching();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseMySQL(builder.Configuration.GetConnectionString("Default")!));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<RegistrationUserNameFilterAttribute>();
builder.Services.AddScoped<RegistrationRoleFilterAttribute>();
builder.Services.AddScoped<RegistrationEmailFilterAttribute>();
builder.Services.AddScoped<LoginUserValidationFilterAttribute>();
builder.Services.AddScoped<ValidateRefreshTokenFilterAttribute>();
builder.Services.AddScoped<AccessTokenValidationFilterAttribute>();
builder.Services.AddScoped<EmailExistsFilterAttribute>();
builder.Services.AddScoped<UserExistsFilterAttribute>();
builder.Services.AddScoped<MatchPasswordsFilterAttribute>();
builder.Services.AddScoped<UserInformationHeaderFilterAttribute>();
builder.Services.AddScoped<HeaderAccessTokenFilterAttribute>();
builder.Services.AddScoped<HeaderRefreshTokenFilterAttribute>();
builder.Services.AddScoped<UserNameExistsFilterAttribute>();
// Auth
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("ApiSettings:Secret")!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer= builder.Configuration.GetValue<string>("ApiSettings:Issuer")!,
        ValidAudiences = builder.Configuration.GetSection("ApiSettings:Audience").Get<List<string>>()!,
        ClockSkew= TimeSpan.Zero
    };
});

var app = builder.Build();

app.UseSwagger(c =>
    {
        c.RouteTemplate = "api/v1/auth/doc/swagger/{documentName}/swagger.json";
    }
    );

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/api/v1/auth/doc/swagger/v1/swagger.json", "Authentication v1");
    options.RoutePrefix = "api/v1/auth/doc/swagger";
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<UpdateNumRequests>();

ApplyMigration();
Console.WriteLine("I am running");
app.Run();
return;

void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
}