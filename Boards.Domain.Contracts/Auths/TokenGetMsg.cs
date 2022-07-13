using System;
using System.Linq;

using Board.Domain.DTO.Jwt;
using Board.Domain.DTO.Users;

namespace Boards.Domain.Contracts.Auths {
	public record TokenGetMsg : AbstractMsg {

		public TokenGetMsg() {}

		public TokenGetMsg(LoginDTO item, AuthSettings auth) {
			this.Item = item;
			this.Auth = auth;
		}

		public LoginDTO Item { get; set; }

		public AuthSettings Auth { get; set; }

	}

	public record TokenGetResponse : AbstractResponse {

		public JwtTokenDTO Token { get; set; }

	}
}
