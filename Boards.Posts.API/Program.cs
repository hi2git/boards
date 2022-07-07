
using Boards.Infrastructure.Web;
using Boards.Posts.API.Consumers;
using Boards.Posts.Application.Queries;
using Boards.Posts.Infrastructure;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var assemblies = new[] { typeof(PostGetAllQuery).Assembly };

services.AddControllers();

services
	.AddInfrastructureRepos(builder.Configuration)
	.AddInfrastructureWeb(assemblies: assemblies, n => n.AddConsumers(typeof(PostGetAllQueryConsumer).Assembly))
;

var app = builder.Build();

//app.MigrateDatabase();

app.UseInfrastructureWeb();


app.Run();
