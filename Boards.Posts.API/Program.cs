
using Board.Infrastructure.Repository;

using Boards.Infrastructure.Web;
using Boards.Posts.API.Consumers;
using Boards.Posts.Application.Queries;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var assemblies = new[] { typeof(PostGetAllQuery).Assembly };

services.AddControllers();

services
	.AddInfrastructureRepos(builder.Configuration)
	.AddInfrastructureWeb(assemblies: assemblies, n => n.AddConsumers(typeof(PostGetAllQueryConsumer).Assembly))
;

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {

//}

//app.MapControllers();
app.UseInfrastructureWeb();


app.Run();
