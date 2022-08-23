using System;
using System.Linq;

namespace Boards.Domain.Contracts.Boards {
	public record BoardDeleteMsg : AbstractMsg {
		public BoardDeleteMsg() {}

		public BoardDeleteMsg(Guid id, Guid userId) : base(id) => this.UserId = userId;

		public Guid UserId { get; set; }
	}

	public record BoardDeleteResponse : AbstractResponse { }
}
