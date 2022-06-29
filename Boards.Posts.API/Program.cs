
using Board.Domain;
using Board.Infrastructure.Files;
using Board.Infrastructure.Repository;

using Boards.Infrastructure.Web;
using Boards.Posts.API.Consumers;
using Boards.Posts.Application.Queries;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var assemblies = new[] { typeof(PostGetAllQuery).Assembly };

services.AddControllers();

services.Configure<AppSettings>(config.GetSection("appSettings"));
services
	.AddInfrastructureFiles()	// TODO: Move to Boards.Files
	.AddInfrastructureRepos(builder.Configuration)
	.AddInfrastructureWeb(assemblies: assemblies, n => n.AddConsumers(typeof(PostGetAllQueryConsumer).Assembly))
;

var app = builder.Build();


app.UseInfrastructureWeb();


app.Run();
