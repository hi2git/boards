using System;

namespace Boards.Domain.Contracts.Posts {
	public record PostDeleteMsg {

		public Guid BoardId { get; set; }

		public Guid Id { get; set; }

	}

	public record PostDeleteResponse : AbstractResponse { }
}
