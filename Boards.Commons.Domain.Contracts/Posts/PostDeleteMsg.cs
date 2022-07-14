using System;

namespace Boards.Domain.Contracts.Posts {
	public record PostDeleteMsg : AbstractMsg {


		public Guid BoardId { get; set; }


	}

	public record PostDeleteResponse : AbstractResponse { }
}
