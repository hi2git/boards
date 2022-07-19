using Boards.Boards.API.Consumers;
using Boards.Boards.Application.Queries;
using Boards.Boards.Infrastructure;
using Boards.Infrastructure.Web;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var assemblies = new[] { typeof(BoardGetAllQuery).Assembly };

builder.AddWeb(assemblies, "Boards", n => n.AddConsumers(typeof(BoardGetAllConsumer).Assembly));

services
	.AddRepos(builder.Configuration)
	//.AddWeb(builder.Logging, assemblies, "Boards", n => n.AddConsumers(typeof(BoardGetAllConsumer).Assembly))
;

var app = builder.Build();

app.Services.Migrate();
app.UseInfrastructureWeb();

app.Run();