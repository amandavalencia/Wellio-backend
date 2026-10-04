
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Wellio.Data;
using Wellio.Models;
using Wellio.Repositories;
using Wellio.Repositories.IRepository;
using Wellio.Services;
using Wellio.Services.IServices;

namespace Wellio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<WellioDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddControllers();
            builder.Services.AddScoped<IActivityService, ActivityService>();
            builder.Services.AddScoped<IMoodService, MoodService>();
            builder.Services.AddScoped<ISleepService, SleepService>();
            builder.Services.AddScoped<IActivityRepo, ActivityRepo>();
            builder.Services.AddScoped<IMoodRepo, MoodRepo>();
            builder.Services.AddScoped<ISleepRepo, SleepRepo>();
            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<WellioDBContext>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddAuthorization();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendDev", policy =>
                {
                    policy.WithOrigins(builder.Configuration["Frontend_Domain"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();

                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseCors("FrontendDev");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            // Identity skapar bland annat /api/auth/register och /api/auth/login.
            app.MapGroup("/api/auth").MapIdentityApi<ApplicationUser>();

            app.Run();
        }
    }
}
