using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;

using Board.Domain.DTO.Users;
using Board.Infrastructure.Jwt.Constants;

using Microsoft.IdentityModel.JsonWebTokens;

namespace Board.Infrastructure.Jwt.Extensions {

	internal static class UserExt {

		public static ClaimsIdentity BuildClaims(this UserLoginDTO user) {
			var role = user?.Role ?? throw new ArgumentNullException(nameof(user));

			var claims = new List<Claim> {
				new (ClaimTypes.Name, user.Name),
				new (ClaimTypes.NameIdentifier, user.Id.ToString()),
				//new Claim(ClaimTypes.Surname, user.Name),
				new (ClaimsIdentity.DefaultRoleClaimType, role.Name),
				new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
				new (ClaimNames.RoleId, role.Id.ToString("D"))
			};

			return new ClaimsIdentity(claims, "token");
		}

	}
}
