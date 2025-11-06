using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVc.Context;
using MVc.Filters;
using MVc.Middlewares;
using MVc.Repositories;
using MVc.Services;

namespace WebApplication1
{
    public class Program
    {
        //7f4d6f43-5b82-4c68-9df1-9f2b5b07d4e0

        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ActionLoggingFilter));
            });


            builder.Services.AddSingleton<SchoolContext>();

            // Register services
            builder.Services.AddScoped<IStudentService, StudentService>();


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();


            app.UseRouting();


            // Custom middleware that logs each request URL
            app.UseMiddleware<RequestLoggingMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "studentDetails",
                pattern: "Student/Details/{id:int}",
                defaults: new { controller = "Student", action = "Details" });


                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            app.Run();
        }
    }
}
