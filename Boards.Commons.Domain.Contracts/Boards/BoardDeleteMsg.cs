using System;
using System.Linq;

namespace Boards.Domain.Contracts.Boards {
	public record BoardDeleteMsg : AbstractMsg {
		public BoardDeleteMsg() {}

		public BoardDeleteMsg(Guid id) : base(id) { }
	}

	public record BoardDeleteResponse : AbstractResponse { }
}
