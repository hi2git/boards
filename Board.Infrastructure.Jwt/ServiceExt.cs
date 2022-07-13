using System;
using System.Text;

using Board.Infrastructure.Jwt.Implementation;
using Board.Infrastructure.Jwt.Middlewares;

using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Jwt;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Board.Infrastructure.Jwt {
	public static class ServiceExt {
		public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration) {
			if (configuration == null)
				return;


			services.AddTransient<ICookieService, CookieService>();
			services.AddTransient<IUserManager, UserManager>();

			var authOptions = configuration.GetSection("authSettings").Get<AuthSettings>();

			services.AddApiJwtAuthentication(authOptions);
			services.Configure<AuthSettings>(configuration.GetSection("authSettings"));
		}

		private static IServiceCollection AddApiJwtAuthentication(this IServiceCollection services, AuthSettings auth) {
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
					ValidateIssuer = false,
					ValidateIssuerSigningKey = true,
					RequireExpirationTime = true,
					ValidateLifetime = true,
					ValidAudience = auth.Audience,
					ValidIssuer = auth.Issuer,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(auth.Secret))
				};
			});

			return services;
		}

		public static IApplicationBuilder UseSecureJwt(this IApplicationBuilder builder) => builder.UseMiddleware<JwtMiddleware>();

	}
}
