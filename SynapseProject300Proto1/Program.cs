
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
            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                        "http://127.0.0.1:5500",
                        "http://localhost:5500",
                        //5500 is for old prototype, keeping it incase
                        "http://localhost:4200"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SynapseDbContext>();

                Console.WriteLine("Database: " + db.Database.GetDbConnection().Database);
                Console.WriteLine("Server: " + db.Database.GetDbConnection().DataSource);
                Console.WriteLine("Vault Count: " + db.Vaults.Count());
            }

            //Swagger - enabled for testing on EC2
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();
            //commented out to use http for prototype

            app.UseCors("AllowFrontend"); //allows frontend to access backend api
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
