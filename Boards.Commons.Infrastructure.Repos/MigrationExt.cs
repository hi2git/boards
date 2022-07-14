using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Boards.Common.Infrastructure.Repos {
	public static class MigrationExt {

		public static IServiceProvider MigrateDb<T>(this IServiceProvider services) where T : DbContext {
			using var scope = services.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<T>();

			context.Database.Migrate();

			return services;
		}

	}
}
