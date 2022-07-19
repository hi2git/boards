
using Boards.Infrastructure.Web;
using Boards.Posts.API.Consumers;
using Boards.Posts.Application.Queries;
using Boards.Posts.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var assemblies = new[] { typeof(PostGetAllQuery) };

builder.Configure("Posts", assemblies, typeof(PostGetAllConsumer));

builder.Services.AddRepos(config);

var app = builder.Build();

app.UseInfrastructureWeb();
app.Services.Migrate();

app.Run();
