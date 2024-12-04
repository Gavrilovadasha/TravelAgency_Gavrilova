using DataGrid.Contracts;
using DataGrid.Contracts.Interface;
using DataGrid.Storage.Memory;
using DataGrid.DataStorage.Entity;
using DataGrid.TourManagment;
using Serilog;

namespace TravelAgency.WebApplication
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            // Проверка соединения с базой данных
            using (var context = new DataGridContext())
            {
                var canConnect = context.Database.Exists();
                Console.WriteLine($"Can connect to database: {canConnect}");
            }

            using (var context = new DataGridContext())
            {
                var tours = context.Tours.ToList();
                Console.WriteLine($"Tours count: {tours.Count}");
            }


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Seq("http://localhost:5341", apiKey: "UnT4YNRs687dCwJZa54N")
                .CreateLogger();

            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<ITourStorage, DBTourStorage>();
            builder.Services.AddScoped<ITourManagment, TourManagment>();
            builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Tours/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Tours}/{action=Index}/{id?}");


            app.Run();
        }
    }

}
