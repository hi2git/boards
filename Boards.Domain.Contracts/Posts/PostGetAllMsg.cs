using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Commons.Domain.DTOs.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostGetAllMsg : AbstractMsg {

		public PostGetAllMsg() { }

		public PostGetAllMsg(Guid id) : base(id) { }

	}

	public record PostGetAllResponse : AbstractResponse {
		public IEnumerable<PostDTO> Items { get; set; } = Enumerable.Empty<PostDTO>();
	}
}
