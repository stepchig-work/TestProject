using Newtonsoft.Json;
using System.Net;
using TestProject.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.MapGet("/", () =>
{
	var returnStatus = new ReturnStatusModel() { Status = "Ok" };
	return JsonConvert.SerializeObject(returnStatus); ;
});

app.MapGet("/healthcheck", () =>
{
	var returnStatus = new ReturnStatusModel() { Status = "Ok" };
	return JsonConvert.SerializeObject(returnStatus); ;
});

app.Run();
