using Board.Infrastructure.Repository;

using Boards.Posts.API.Consumers;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddInfrastructureRepos(builder.Configuration)
;

services.AddHealthChecks();

services.AddMassTransit(n => {
	n.AddConsumer<GetAllQueryConsumer>();

	n.UsingRabbitMq((context, cfg) => {
		cfg.Host("rabbitmq", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
		cfg.ConfigureEndpoints(context);
	});
});

services.AddOptions<MassTransitHostOptions>()
	.Configure(options => {
		options.WaitUntilStarted = true;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection()
	.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/hc");

app.Run();
