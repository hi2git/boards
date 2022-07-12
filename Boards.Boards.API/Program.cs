using Boards.Boards.API.Consumers;
using Boards.Boards.Application.Queries;
using Boards.Boards.Infrastructure;
using Boards.Infrastructure.Web;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var assemblies = new[] { typeof(BoardGetAllQuery).Assembly };

services
	.AddRepos(builder.Configuration)
	.AddInfrastructureWeb(assemblies: assemblies, n => n.AddConsumers(typeof(BoardGetAllConsumer).Assembly))
;

var app = builder.Build();

app.Services.Migrate();
app.UseInfrastructureWeb();

app.Run();