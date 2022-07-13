using System;
using System.Linq;

namespace Boards.Domain.Contracts.Users {
	public record UserDeleteMsg : AbstractMsg {
		public UserDeleteMsg() { }

		public UserDeleteMsg(Guid id) : base(id) { }
	}

	public record UserDeleteResponse : AbstractResponse { }
}
