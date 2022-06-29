using System;
using System.Linq;

using Board.Domain.DTO.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostCreateMsg : AbstractMsg {
		

		public PostDTO Item { get; set; }
	}

	public record PostCreateResponse : AbstractResponse{}
}
