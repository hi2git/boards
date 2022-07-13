using System;
using System.Linq;

using Board.Domain.DTO.Users;

namespace Boards.Domain.Contracts.Users {
	public record UserCreateMsg : AbstractMsg {

		public UserCreateMsg() { }

		public UserCreateMsg(LoginDTO item) => this.Item = item;

		public LoginDTO Item { get; set;  }
	}

	public record UserCreateResponse : AbstractResponse { }
}
