using System;

namespace Boards.Commons.Domain.DTOs.Users {
	public record LoginDTO {

		public LoginDTO() { }

		public string Login { get; set; }
		public string Password { get; set; }

		public string Email { get; set; }

	}
}
