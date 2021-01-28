using System;

using Board.Domain.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Board.Infrastructure.Files {
	public static class ServicesExt {

		public static void AddFiles(this IServiceCollection services) {
			services.AddScoped<IFileStorage, FileStorage>();
		}

	}
}
