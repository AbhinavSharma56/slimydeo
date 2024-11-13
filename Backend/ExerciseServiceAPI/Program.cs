
using ExerciseServiceAPI.Data;
using ExerciseServiceAPI.Repository;
using ExerciseServiceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ExerciseServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IExerciseLogRepository, ExerciseLogRepository>();
            builder.Services.AddTransient<IExerciseTypeRepository, ExerciseTypeRepository>();
            builder.Services.AddTransient<IExerciseService, ExerciseService>();

            builder.Services.AddHttpClient("Nutritionix", client =>
            {
                client.BaseAddress = new Uri("https://trackapi.nutritionix.com/v2/");
                client.DefaultRequestHeaders.Add("x-app-id", "ab8d7a7d");
                client.DefaultRequestHeaders.Add("x-app-key", "f31a5109e87b041901b887269b07225c");
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ExerciseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ExerciseDB")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
