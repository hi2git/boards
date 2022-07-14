using System;

namespace Boards.Domain.Contracts.Images {
	public record ImageGetMsg : AbstractMsg {

		public ImageGetMsg() {}

		public ImageGetMsg(Guid id) : base(id) { }
	}

	public record ImageGetResponse : AbstractResponse {

		public string Content { get; set; } = string.Empty;

	}
}
