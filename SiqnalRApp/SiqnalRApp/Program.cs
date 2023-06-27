using Microsoft.Extensions.Configuration;
using SiqnalRApp.bussiness;
using SiqnalRApp.Hubs;
using SiqnalRApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyHeader()
																		   .AllowAnyMethod()
																		   .AllowCredentials()
																		   .SetIsOriginAllowed(origin => true)));
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapHub<MyHub>("/myHub"));
app.UseEndpoints(endpoints => endpoints.MapHub<MessageHub>("/messageHub"));
app.UseEndpoints(endpoints => endpoints.MapHub<ChatHub>("/chatHub"));
app.UseEndpoints(endpoints => endpoints.MapHub<RabbitMessageHub>("/rabbitHub"));

app.MapControllers();
app.Run();
