
using Microsoft.EntityFrameworkCore;
using SynapseProject300Proto1.Controllers.Model;

namespace SynapseProject300Proto1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SynapseDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("SynapseConnection")));

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500").AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SynapseDbContext>();

                if (db.Database.CanConnect())
                {
                    Console.WriteLine("Successfully connected to Synapse SQL Server!");
                }
                else
                {
                    Console.WriteLine("Failed to connect to Synapse SQL Server.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            //commented out to use http for prototype

            app.UseCors("AllowFrontend"); //allows frontend to access backend api
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
