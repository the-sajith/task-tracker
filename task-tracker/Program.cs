
using Microsoft.EntityFrameworkCore;
using task_tracker.Data;

namespace task_tracker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			var connectionString = builder.Configuration.GetRequiredSection("ConnectionStrings:DefaultConnection").Value;
			// Add services to the container.

			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<TaskDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
				
			});

            builder.Services.AddCors(options =>
			{
				options.AddPolicy("Cors Policy", builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyMethod();
					builder.AllowAnyHeader();
				});
			});

			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo());
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
			}

			app.UseCors("Cors Policy");

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks API");
			});

			app.UseStaticFiles();
			app.UseRouting();


			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}");

			app.MapFallbackToFile("index.html");

			app.Run();
		}
	}
}