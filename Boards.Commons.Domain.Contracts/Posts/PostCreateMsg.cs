using System;
using System.Linq;

using Boards.Commons.Domain.DTOs.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostCreateMsg : AbstractMsg {

		public PostCreateMsg() { }

		public PostCreateMsg(Guid boardId) : base(boardId) { }


		public PostDTO Item { get; set; }
	}

	public record PostCreateResponse : AbstractResponse{}
}
