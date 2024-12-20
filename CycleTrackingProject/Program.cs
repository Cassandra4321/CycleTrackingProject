using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CycleTrackingProject.Data;
using DbContext = CycleTrackingProject.Data.DbContext;
using CycleTrackingProject.Areas.Identity.Data;
namespace CycleTrackingProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DbContextConnection") ?? throw new InvalidOperationException("Connection string 'DbContextConnection' not found.");

            // L�gg till DbContext
            builder.Services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(connectionString));

            // L�gg till Identity med DefaultUI
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DbContext>()
                .AddDefaultUI();  // L�gg till denna rad f�r att f� med UI-sidorna

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
