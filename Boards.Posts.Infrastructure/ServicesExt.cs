using System;

using Board.Domain.Repos;

using Boards.Posts.Domain.Repos;
using Boards.Posts.Infrastructure.Repos;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boards.Posts.Infrastructure {
	public static class ServicesExt {

		public static IServiceCollection AddInfrastructureRepos(this IServiceCollection services, IConfiguration config) {
			services.AddDbContext<PostsContext>(options =>
				options.UseSqlServer(config.GetConnectionString("Db"), opt => {
					opt.CommandTimeout(120);
					opt.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
				})
			);

			services.AddScoped<IPostRepo, PostRepo>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			

			return services;

		}

	}
}
