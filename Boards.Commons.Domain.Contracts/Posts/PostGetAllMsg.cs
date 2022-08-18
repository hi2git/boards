using System;
using System.Linq;

using Boards.Commons.Domain.DTOs;
using Boards.Commons.Domain.DTOs.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostGetAllMsg : AbstractMsg {

		public PostGetAllMsg() { }

		public PostGetAllMsg(PostFilter filter) : base(filter.BoardId) => this.Filter = filter;

		public PostFilter Filter { get; set; }


	}

	public record PostGetAllResponse : AbstractResponse {
		public Pageable<PostDTO> Page { get; set; }
	}
}
