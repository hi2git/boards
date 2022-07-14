using System;
using System.Linq;

namespace Boards.Domain.Contracts.Users {
	public record UserDeletedEvent : AbstractMsg {
		public UserDeletedEvent() {}

		public UserDeletedEvent(Guid id) : base(id) {}
	}
}
