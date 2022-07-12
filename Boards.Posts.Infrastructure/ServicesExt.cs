using System;

using Board.Domain.Repos;
using Board.Infrastructure.Repository;

using Boards.Posts.Domain.Repos;
using Boards.Posts.Infrastructure.Repos;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boards.Posts.Infrastructure {
	public static class ServicesExt {

		public static IServiceCollection AddRepos(this IServiceCollection services, IConfiguration config) {
			services.AddDbContext<PostsContext>(options =>
				options.UseSqlServer(config.GetConnectionString("Db"), opt => {
					opt.CommandTimeout(120);
					opt.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
				})
			);

			services.AddTransient<IPostRepo, PostRepo>();
			services.AddTransient<IEventRepo, EventRepo>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}

		public static IServiceProvider Migrate(this IServiceProvider app) => app.MigrateDb<PostsContext>();

	}
}
