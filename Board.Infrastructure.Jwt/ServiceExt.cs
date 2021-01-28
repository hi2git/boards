using System;

using Board.Domain.Services;
using Board.Infrastructure.Jwt.Implementation;
using Board.Infrastructure.Jwt.Interfaces;
using Board.Infrastructure.Jwt.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Board.Infrastructure.Jwt {
	public static class ServiceExt {
		public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration) {
			if (configuration == null)
				return;

			services.AddScoped();

			var section = configuration.GetSection("authSettings");
			var options = section.Get<AuthSettings>();
			var jwtOptions = new JwtOptions(options.Audience, options.Issuer, options.Secret, options.Lifetime);
			services.AddApiJwtAuthentication(jwtOptions);
			services.Configure<AuthSettings>(section);
		}

		private static IServiceCollection AddApiJwtAuthentication(this IServiceCollection services, JwtOptions tokenOptions) {
			if (tokenOptions == null)
				throw new ArgumentNullException($"{nameof(tokenOptions)} is a required parameter. Please make sure you've provided a valid instance with the appropriate values configured.");

			services.AddScoped<ITokenService, TokenService>(_ => new TokenService(tokenOptions));

			services.AddAuthentication(options => {
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters {
					ClockSkew = TimeSpan.Zero,
					ValidateAudience = false,
					ValidAudience = tokenOptions.Audience,
					ValidateIssuer = false,
					ValidIssuer = tokenOptions.Issuer,
					IssuerSigningKey = tokenOptions.SigningKey,
					ValidateIssuerSigningKey = true,
					RequireExpirationTime = true,
					ValidateLifetime = true
				};
			});

			return services;
		}

		private static IServiceCollection AddScoped(this IServiceCollection services) {
			services.AddTransient<ICookieService, CookieService>();
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IUserManager, UserManager>();

			return services;
		}

	}
}
