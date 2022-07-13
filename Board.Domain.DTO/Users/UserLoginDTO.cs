using System;

using Board.Domain.Enums;

namespace Boards.Commons.Domain.DTOs.Users {
	public class UserLoginDTO {

		public Guid Id { get; set; }
		public string Name { get; set; }
		//public string PasswordHash { get; set; }

		public string Role { get; set; }

	}

}
