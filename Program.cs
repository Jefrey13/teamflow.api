using AspNetCoreRateLimit;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using teamflow.API.Data;
using teamflow.API.Middleware;
using teamflow.API.Repositories.Implementations;
using teamflow.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer();

// Cookie Policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

// Repositories DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
builder.Services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
builder.Services.AddScoped<IProjectFileRepository, ProjectFileRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    builder.Configuration.GetSection("IpRateLimiting").Bind(options);
    options.QuotaExceededResponse = new QuotaExceededResponse
    {
        Content = "{{ \"success\": false, \"message\": \"Has excedido el límite de {0} llamadas por {1}. Espera {2} segundos antes de reintentar.\" }}",
        ContentType = "application/json",
        StatusCode = 429
    };
});
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// AutoMapper + FluentValidation
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Health Checks
builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "sqlserver",
        failureStatus: HealthStatus.Unhealthy);

// Swagger/OpenAPI with JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectManagement.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Usa 'Bearer {token}' en el header Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        policy
            .WithOrigins("https://localhost:5173")
            .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
            .WithHeaders("Content-Type", "Authorization")
            .AllowCredentials();
    });
    options.AddPolicy("LoginCors", policy =>
    {
        policy
            .WithOrigins("https://localhost:5173")
            .WithMethods("POST")
            .WithHeaders("Content-Type", "Authorization");
    });
});

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HSTS in Production
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

// Global Error Handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// Security Headers
app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
    ctx.Response.Headers["X-Frame-Options"] = "DENY";
    ctx.Response.Headers["X-Content-Type-Options"] = "nosniff";
    ctx.Response.Headers["Content-Security-Policy"] = "default-src 'self'";
    await next();
});

app.UseCookiePolicy();

app.UseRouting();

app.UseCors("DefaultCors");

app.UseIpRateLimiting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();