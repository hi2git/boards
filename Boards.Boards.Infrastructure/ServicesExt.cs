using System;

using Board.Domain.Repos;

using Boards.Boards.Infrastructure.Repos;
using Boards.Boards.Domain.Repos;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boards.Boards.Infrastructure {
	public static class ServicesExt {

		public static IServiceCollection AddInfrastructureRepos(this IServiceCollection services, IConfiguration config) {
			services.AddDbContext<BoardsContext>(options =>
				options.UseSqlServer(config.GetConnectionString("Db"), opt => {
					opt.CommandTimeout(120);
					opt.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
				})
			);

			services.AddScoped<IBoardRepo, BoardRepo>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();


			return services;

		}

	}
}
