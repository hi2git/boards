using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Board.Domain.DTO.Jwt;
using Board.Domain.DTO.Users;

using Boards.Users.Application;

using Microsoft.IdentityModel.Tokens;

namespace Boards.Users.Infrastructure.Services {
	internal class TokenService : ITokenService {

		public Task<JwtTokenResult> Generate(UserLoginDTO user, AuthSettings auth) {
			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(auth.Secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiration = TimeSpan.FromMinutes(auth.Lifetime);
			var claimsIdentity = BuildClaims(user);

			var jwt = new JwtSecurityToken(
				issuer: auth.Issuer,
				audience: auth.Audience,
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

		public static ClaimsIdentity BuildClaims(UserLoginDTO user) {
			var claims = new List<Claim> {
				new (ClaimTypes.Name, user.Name),
				new (ClaimTypes.NameIdentifier, user.Id.ToString()),
				new (ClaimTypes.Role, user.Role),
				new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
			};

			return new ClaimsIdentity(claims, "token");
		}

	}
}
