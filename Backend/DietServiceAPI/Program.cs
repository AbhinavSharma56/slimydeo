using DietServiceAPI.Data;
using DietServiceAPI.Repository;
using DietServiceAPI.Service;
using Microsoft.EntityFrameworkCore;

namespace DietServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.
            builder.Services.AddScoped<IMealRepository, MealRepository>();
            builder.Services.AddScoped<IFoodRepository, FoodRepository>();
            builder.Services.AddScoped<IFoodDetailsRepository, FoodDetailsRepository>();
            builder.Services.AddScoped<IFoodDetailsService, FoodDetailsService>();

            builder.Services.AddHttpClient("NutritionixDiet", client =>
            {
                client.BaseAddress = new Uri("https://trackapi.nutritionix.com/v2/");
                client.DefaultRequestHeaders.Add("x-app-id", "ab8d7a7d");
                client.DefaultRequestHeaders.Add("x-app-key", "f31a5109e87b041901b887269b07225c");
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DietDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DietDB")));

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
