using System;

using Boards.Commons.Domain.DTOs.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostContentUpdateMsg : AbstractMsg {


		public PostDTO Item { get; set;  }

	}

	public record PostContentUpdateResponse : AbstractResponse { }
	
}
