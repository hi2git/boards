using System;

namespace Boards.Commons.Domain.DTOs.Jwt {
	public record AuthSettings {

		public string Issuer { get; set; }

		public string Audience { get; set; }

		public int Lifetime { get; set; }

		public string Secret { get; set; }

	}
}
