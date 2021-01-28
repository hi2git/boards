using System;

using Board.Domain.Repos;
using Board.Infrastructure.Repository.Implementation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Board.Infrastructure.Repository {
	public static class ServicesExt {

		public static void AddDbContext(this IServiceCollection services, IConfiguration configuration) {
			services.AddDbContext<BoardContext>(options =>
				options//.UseLazyLoadingProxies()
				.UseSqlServer(configuration.GetConnectionString("Db"), opt => {
					opt.CommandTimeout(120);
					opt.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
				})
			);

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IRepo<>), typeof(AbstractRepo<>));

			services.AddScoped<IBoardItemRepo, BoardItemRepo>();
			services.AddScoped<IUserRepo, UserRepo>();

		}

	}
}
