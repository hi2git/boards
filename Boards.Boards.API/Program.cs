using Boards.Boards.API.Consumers;
using Boards.Boards.Application.Queries;
using Boards.Boards.Infrastructure;
using Boards.Infrastructure.Web;

var builder = WebApplication.CreateBuilder(args);

var handlers = new[] { typeof(BoardGetAllQuery) };
builder.Configure("Boards", handlers, filters: null, typeof(BoardGetAllConsumer));

builder.Services.AddRepos(builder.Configuration);

var app = builder.Build();

app.UseInfrastructureWeb();
app.Services.Migrate();

app.Run();