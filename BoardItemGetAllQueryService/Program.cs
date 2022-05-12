using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Infrastructure.Repository;

using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoardItemGetAllQueryService {
	public class Program {
		public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) => {
					services.AddInfrastructureRepos(hostContext.Configuration);

					services.AddMassTransit(n => {
						n.AddConsumer<BoardItemGetAllQueryConsumer>();

						n.UsingRabbitMq((context, cfg) => {
							cfg.Host("rabbitmq", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
							cfg.ConfigureEndpoints(context);
						});
					});

					services.AddOptions<MassTransitHostOptions>()
						.Configure(options => {
							options.WaitUntilStarted = true;
						});

				});
	}
}
