using System;

namespace Board.Infrastructure.Jwt.Models {
	internal class AuthSettings {

		public string Issuer { get; set; }

		public string Audience { get; set; }

		public int Lifetime { get; set; }

		public string Secret { get; set; }

	}
}
