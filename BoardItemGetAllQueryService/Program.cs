using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Boards.Infrastructure.Queues;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoardItemGetAllQueryService {
	public class Program {
		public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) => {
					services.AddHostedService<Worker>();

					services.AddInfrastructureQueues();
					services.AddTransient<Consumer>();
				});
	}
}
