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

		public Task<JwtTokenResult> Generate(UserLoginDTO user) {
			var expiration = TimeSpan.FromMinutes(_tokenOptions.TokenExpiryInMinutes);
			var claimsIdentity = user.BuildClaims();
			var creds = new SigningCredentials(_tokenOptions.SigningKey, SecurityAlgorithms.HmacSha256);

			var jwt = new JwtSecurityToken(
				issuer: _tokenOptions.Issuer,
				audience: _tokenOptions.Audience,
				claims: claimsIdentity.Claims,
				notBefore: DateTime.UtcNow,
				expires: DateTime.UtcNow.Add(expiration),
				signingCredentials: creds
			);

			var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

			return Task.FromResult(new JwtTokenResult {
				AccessToken = accessToken,
				Expires = expiration
			});
		}
	}
}
