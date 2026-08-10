using Microsoft.EntityFrameworkCore;
using regisztracio_be.Context;

namespace regisztracio_be
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddOpenApi();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowNgrok", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var connection = String.Empty;
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
                connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            }
            else
            {
                connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
            }

            builder.Services.AddDbContext<OTDbContext>(options =>
                options.UseSqlServer(connection));

            var app = builder.Build();
            app.UseCors("AllowNgrok");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<OTDbContext>();
                    context.Database.Migrate(); // Létrehozza a táblákat az adatbázisban
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba a migráció során: {ex.Message}");
                }
            }

            app.Run();
        }
    }
}
