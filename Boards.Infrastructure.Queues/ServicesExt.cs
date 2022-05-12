using System;
using System.Collections.Generic;
using System.Linq;

using Board.Application.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Boards.Infrastructure.Queues {
	public static class ServicesExt {

		public static IServiceCollection AddInfrastructureQueues(this IServiceCollection services) {
			services.AddTransient<IProducer, Producer>();
			//services.AddTransient<IRpc, Producer>();
			services.AddTransient(typeof(IRpcClient<>), typeof(AbstractRpcClient<>));

			return services.AddInfrastructure();
		}

	}
}
