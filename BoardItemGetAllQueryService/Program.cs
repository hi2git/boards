using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoardItemGetAllQueryService {
	public class Program {
		public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) => {

					services.AddMassTransit(n => {
						n.AddConsumer<BoardItemGetAllQueryConsumer>();

						n.UsingRabbitMq((context, cfg) => {
							cfg.Host("192.168.1.127", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
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
