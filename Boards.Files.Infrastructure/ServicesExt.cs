using System;

using Board.Domain.Repos;

using Boards.Files.Application;
using Boards.Files.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace Board.Infrastructure.Files {
	public static class ServicesExt {

		public static IServiceCollection AddInfrastructureFiles(this IServiceCollection services) {
			services.AddScoped<IFileStorage, FileStorage>();
			services.AddScoped<IEventRepo, EventRepo>();

			return services;
		}

	}
}
