using BetWebApp_2.Components;
using BetWebApp_2.Data;
using BetWebApp_2.Models;
using BetWebApp_2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace BetWebApp_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var dbFileName = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? "app.db";

            var dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            var absoluteDbPath = Path.Combine(dataFolder, dbFileName);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "auth_token";
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/access-denied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddSingleton<PasswordService>();
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlite($"Data Source={absoluteDbPath}"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                using var db = factory.CreateDbContext();
                db.Database.Migrate();

                var adminPassword = app.Configuration["Seed:AdminPassword"];
                if (!string.IsNullOrEmpty(adminPassword) && !db.Users.Any(u => u.Role == UserRole.Admin))
                {
                    var passwords = scope.ServiceProvider.GetRequiredService<PasswordService>();
                    var admin = new User { Username = "admin", Role = UserRole.Admin };
                    admin.PasswordHash = passwords.Hash(admin, adminPassword);
                    db.Users.Add(admin);
                    db.SaveChanges();
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
