using System;

namespace Board.Domain.DTO.Users {
	public record LoginDTO {

		public LoginDTO() { }

		public string Login { get; set; }
		public string Password { get; set; }

		public string Email { get; set; }

	}
}
