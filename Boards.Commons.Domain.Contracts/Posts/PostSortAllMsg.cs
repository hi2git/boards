using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Commons.Domain.DTOs.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostSortAllMsg : AbstractMsg {
		public PostSortAllMsg() { }

		public PostSortAllMsg(Guid boardId) : base(boardId) { }

		public IEnumerable<PostDTO>? Items { get; set;  }

	}

	public record PostSortedResponse : AbstractResponse { }
}
