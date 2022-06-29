using System;

using Board.Domain.DTO.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostUpdateMsg  {

		public PostDTO Item { get; set; }

	}


	public record PostUpdateResponse : AbstractResponse { }
}
