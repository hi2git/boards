using System;

namespace Boards.Domain.Contracts.Images {
	public record ImageUpdateMsg : AbstractMsg {

		public ImageUpdateMsg() : base() { }

		public ImageUpdateMsg(Guid id, string content) : base(id) => this.Content = content;

		public string Content { get; set; }
	}

	public record ImageUpdateResponse : AbstractResponse { }
}
