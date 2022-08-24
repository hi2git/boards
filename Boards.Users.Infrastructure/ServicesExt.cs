using System;

using Board.Domain.Repos;

using Boards.Commons.Infrastructure.Repos;
using Boards.Users.Application;
using Boards.Users.Domain.Repos;
using Boards.Users.Infrastructure.Repos;
using Boards.Users.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boards.Posts.Infrastructure {
	public static class ServicesExt {

		public static IServiceCollection AddRepos(this IServiceCollection services, IConfiguration config) {
			services.AddDbContext<UsersContext>(options =>
				options.UseSqlServer(config.GetConnectionString("Db"), opt => {
					opt.CommandTimeout(120);
					opt.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
				})
			);

			services.AddTransient<IUserRepo, UserRepo>();
			services.AddTransient<IEventRepo, EventRepo>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IPasswordService, PasswordService>();
			services.AddScoped<ITokenService, TokenService>();

			return services;
		}
		
		public static IServiceProvider Migrate(this IServiceProvider app) => app.MigrateDb<UsersContext>();

	}
}
