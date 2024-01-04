using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestProject.Mapper;
using TestProject.Models;
using TestProject.Services;

public class Program
{
	private static IServiceCollection ConfigureServices(IServiceCollection services)
	{
		services.AddTransient<IUserService, UserService>();

		services.AddLogging(loggerBuilder =>
		{
			loggerBuilder.ClearProviders();
			loggerBuilder.AddConsole();
		});

		services.AddAutoMapper(typeof(MapProfiler));

		return services;
	}

		static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);


		ConfigureServices(builder.Services);

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddControllers();

		var app = builder.Build();

		// Configure the HTTP request pipeline.

			app.UseSwagger();
			app.UseSwaggerUI();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}




		app.MapGet("", () =>
		{
			var returnStatus = new ReturnStatusModel() { Status = "Ok" };
			return JsonConvert.SerializeObject(returnStatus); ;
		});

		app.MapGet("/healthcheck", () =>
		{
			var returnStatus = new ReturnStatusModel() { Status = "Ok" };
			return JsonConvert.SerializeObject(returnStatus); ;
		});
		app.MapControllers();

		app.Run();
	}
}