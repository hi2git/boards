using System;

namespace Boards.Domain.Contracts.Posts {
	public record PostDeleteMsg : AbstractMsg {

		public PostDeleteMsg() { }

		public PostDeleteMsg(Guid boardId) : base(boardId) { }


		public Guid PostId { get; set; }


	}

	public record PostDeleteResponse : AbstractResponse { }
}
