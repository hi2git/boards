using System;
using System.Collections.Generic;
using System.Linq;

using Board.Application.Services;

using Boards.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Boards.Infrastructure {
	public static class ServicesExt {

		public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
			services.AddTransient<IJsonService, JsonService>();

			return services;
		}

	}
}
