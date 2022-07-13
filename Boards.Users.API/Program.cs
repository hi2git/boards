using Boards.Infrastructure.Web;
using Boards.Users.API.Consumers;
using Boards.Users.Application.Queries;

using MassTransit;
using Boards.Posts.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var config = builder.Configuration;
var assemblies = new[] { typeof(UserGetAllQuery).Assembly };

services
	.AddRepos(config)
	.AddInfrastructureWeb(assemblies: assemblies, n => n.AddConsumers(typeof(UserGetAllConsumer).Assembly))
;

var app = builder.Build();

app.Services.Migrate();
app.UseInfrastructureWeb();

app.Run();
