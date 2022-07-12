using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Boards.Posts.Infrastructure {
	public static class MigrationExt {

		public static IServiceProvider Migrate(this IServiceProvider services) { 
			using var scope = services.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<PostsContext>();

			context.Database.Migrate();

			return services;
		}

	}
}
