
using Boards.Infrastructure.Web;
using Boards.Posts.API.Consumers;
using Boards.Posts.Application.Queries;
using Boards.Posts.Infrastructure;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var assemblies = new[] { typeof(PostGetAllQuery).Assembly };


services
	.AddInfrastructureRepos(config)
	.AddInfrastructureWeb(assemblies: assemblies, n => n.AddConsumers(typeof(PostGetAllConsumer).Assembly))
;

var app = builder.Build();

app.Services.Migrate();
app.UseInfrastructureWeb();

app.Run();
