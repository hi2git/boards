using System;

using Board.Domain.Enums;

namespace Board.Domain.DTO.Users {
	public class UserLoginDTO {

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string PasswordHash { get; set; }
		public RoleDTO Role { get; set; }
		//public string Name { get; set; }

	}

	public class RoleDTO {
		public RoleEnum Id { get; set; }

		public string Name { get; set; }
	}


}
