using Boards.Infrastructure.Web;
using Boards.Users.API.Consumers;
using Boards.Users.Application.Queries;

using Boards.Posts.Infrastructure;

var handlers = new[] { typeof(UserGetAllQuery) };

var builder = WebApplication.CreateBuilder(args);
builder.Configure("Users", handlers, typeof(UserGetAllConsumer));
builder.Services.AddRepos(builder.Configuration);

var app = builder.Build();

app.Services.Migrate();
app.UseInfrastructureWeb();

app.Run();
