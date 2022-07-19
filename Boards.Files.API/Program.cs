
using Board.Infrastructure.Files;

using Boards.Files.API.Consumers;
using Boards.Files.Application.Commands;
using Boards.Files.Infrastructure;
using Boards.Infrastructure.Web;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

//services.Configure<AppSettings>(config.GetSection("appSettings"));

var assemblies = new[] { typeof(ImageGetQuery).Assembly };

builder.AddWeb(assemblies, "Files", n => n.AddConsumers(typeof(ImageGetConsumer).Assembly));

services
	.AddInfrastructureFiles();
	//.AddWeb(builder.Logging, assemblies, "Files", n => n.AddConsumers(typeof(ImageGetConsumer).Assembly));

services.AddControllers();


var app = builder.Build();

app.UseInfrastructureWeb();

app.Run();

