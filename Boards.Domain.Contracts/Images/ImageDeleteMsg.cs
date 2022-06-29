using System;

namespace Boards.Domain.Contracts.Images {
	public record ImageDeleteMsg : AbstractMsg {

		public ImageDeleteMsg() { }

		public ImageDeleteMsg(Guid id) : base(id) { }

	}

	public record ImageDeleteResponse : AbstractResponse { }
}
