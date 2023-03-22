using BussinessLogic.Implementations;
using BussinessLogic.Interfaces;
using BussinessLogic.MappingProfile;
using BussinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repository;
using DataAccess.Seeds;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(BookMappingProfile));
            builder.Services.AddDbContext<LibraryDbContext>(opts =>
            {
                var defaultConn = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

                opts.UseSqlServer(defaultConn);


            });
            //policy
       /*     builder.Services.AddAuthorization(opts => {
                opts.AddPolicy("AspManager", policy => {
                    policy.RequireRole("Manager");
                    policy.RequireClaim("Coding-Skill", "ASP.NET Core MVC");
                });
            });*/

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<LibraryDbContext>>();
            builder.Services.AddTransient<ILibrarianServices, LibrarianService>();
            builder.Services.AddTransient<ICatalogueServices, CatalogueService>();
            builder.Services.AddTransient<AddUpdateBookVM>();
            builder.Services.AddAutoMapper(Assembly.Load("BussinessLogic"));

            //identity

            //builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
            //builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
            // builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            /*end of identity*/


            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<LibraryDbContext>().AddDefaultTokenProviders();
            //customloginurl
            /*builder.Services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Authenticate/Login");*/

            //access denied
            builder.Services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/Stop/Index";
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //await SeedData.EnsurePopulatedAsync(app);
            app.Run();
        }
    }
}