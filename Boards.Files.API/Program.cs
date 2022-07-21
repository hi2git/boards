
using Board.Infrastructure.Files;

using Boards.Files.API.Consumers;
using Boards.Files.Application.Commands;
using Boards.Files.Infrastructure;
using Boards.Infrastructure.Web;


var builder = WebApplication.CreateBuilder(args);

var handlers = new[] { typeof(ImageGetQuery) };
builder.Configure("Files", handlers, filters: null, typeof(ImageGetConsumer));

builder.Services
	.Configure<AppSettings>(builder.Configuration.GetSection("appSettings"))
	.AddInfrastructureFiles()
	.AddControllers();

builder.Build()
	.UseInfrastructureWeb()
	.Run();

