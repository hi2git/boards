using System;

using Boards.Commons.Domain.DTOs.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostUpdateMsg : AbstractMsg {

		public PostDTO Item { get; set; }

	}


	public record PostUpdateResponse : AbstractResponse { }
}
