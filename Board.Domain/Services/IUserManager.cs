using System;

using Board.Domain.Enums;

namespace Board.Domain.Services {
	public interface IUserManager {

		Guid CurrentUserId { get; }

		/// <summary>Представляет идентификатор роли текущего пользователя</summary>
		RoleEnum CurrentRole { get; }

		string UserName { get; }

		bool IsExist { get; }

	}
}
