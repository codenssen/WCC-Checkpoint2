using Microsoft.EntityFrameworkCore;
using System;
using WCC_Checkpoint2.Data;
using WCC_Checkpoint2.Interface;
using WCC_Checkpoint2.Repository;
using Microsoft.AspNetCore.Identity;
using WCC_Checkpoint2.Areas.Identity.Data;

namespace WCC_Checkpoint2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDb"));
            });    
          
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();

            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

       

            app.Run();
        }
    }
}
