using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

using Board.Domain.DTO.Users;
using Board.Infrastructure.Jwt.Extensions;
using Board.Infrastructure.Jwt.Interfaces;
using Board.Infrastructure.Jwt.Models;

using Microsoft.IdentityModel.Tokens;

namespace Board.Infrastructure.Jwt.Implementation {
	internal class TokenService : ITokenService {
		private readonly JwtOptions _tokenOptions;

		public TokenService(JwtOptions tokenOptions) => _tokenOptions = tokenOptions
			?? throw new ArgumentNullException($"An instance of valid {nameof(JwtOptions)} must be passed in order to generate a JWT!");

		public async Task<JwtTokenResult> Generate(UserLoginDTO user) {
			var expiration = TimeSpan.FromMinutes(_tokenOptions.TokenExpiryInMinutes);
			var claimsIdentity = user.BuildClaims();

			var jwt = new JwtSecurityToken(
				_tokenOptions.Issuer,
				_tokenOptions.Audience,
				claimsIdentity.Claims,
				DateTime.UtcNow,
				DateTime.UtcNow.Add(expiration),
				new SigningCredentials(_tokenOptions.SigningKey, SecurityAlgorithms.HmacSha256)
			);

			var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new JwtTokenResult {
				AccessToken = accessToken,
				Expires = expiration
			};
		}
	}
}
