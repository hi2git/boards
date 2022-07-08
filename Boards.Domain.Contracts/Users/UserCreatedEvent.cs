using System;
using System.Linq;

namespace Boards.Domain.Contracts.Users {
	public record UserCreatedEvent : AbstractMsg {
		public UserCreatedEvent() { }

		public UserCreatedEvent(Guid id) : base(id) { }
	}

}
