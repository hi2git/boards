using System;
using System.Linq;

namespace Boards.Domain.Contracts.Boards {
	public record BoardDeletedEvent : AbstractMsg {

		public BoardDeletedEvent() { }

		public BoardDeletedEvent(Guid id) : base(id) { }

	}
}
