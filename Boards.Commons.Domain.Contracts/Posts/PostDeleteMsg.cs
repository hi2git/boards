using System;

namespace Boards.Domain.Contracts.Posts {
	public record PostDeleteMsg : AbstractMsg {


		public Guid PostId { get; set; }


	}

	public record PostDeleteResponse : AbstractResponse { }
}
