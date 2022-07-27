using System;
using System.Linq;

using Boards.Commons.Domain.DTOs.Users;

namespace Boards.Domain.Contracts.Users {
	public record UserUpdateMsg : AbstractMsg {
		public UserUpdateMsg() {}

		public UserUpdateMsg(Guid id, UserSettingsDTO item) : base(id) => this.Item = item;

		public UserSettingsDTO Item { get; set;  }

	}

	public record UserUpdateResponse : AbstractResponse { }
}
