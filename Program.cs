using Microsoft.Data.SqlClient;
using MVc.Context;
using MVc.Repositories;

namespace WebApplication1
{
    public class Program
    {
        //7f4d6f43-5b82-4c68-9df1-9f2b5b07d4e0

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container. ID
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<SchoolContext, SchoolContext>();
            builder.Services.AddSingleton<StudentRepository, StudentRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())

            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
