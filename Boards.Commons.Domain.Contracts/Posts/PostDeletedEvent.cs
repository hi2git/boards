using System;

namespace Boards.Domain.Contracts.Posts {
	public record PostDeletedEvent : AbstractMsg {
		public PostDeletedEvent() {}

		public PostDeletedEvent(Guid id) : base(id) { }
	}
}
