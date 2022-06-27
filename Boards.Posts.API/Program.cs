
using Board.Infrastructure.Repository;

using Boards.Infrastructure.Web;
using Boards.Posts.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddInfrastructureRepos(builder.Configuration)
	.AddInfrastructureWeb(consumers: typeof(PostGetAllQueryConsumer).Assembly)
;

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
//}

app.MapControllers();

app.MapHealthChecks("/hc");
app.UseInfrastructureWeb();

app.Run();
