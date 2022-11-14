using Board.Domain.Enums;

using Boards.Users.Domain.Models;

namespace Boards.Users.Domain.Tests {
	internal static class UserData {

		public static User Create() => new(ID, NAME, PASSWORD, ROLE, EMAIL);

		public static IEnumerable<object[]> Props {
			get {
				yield return new object[] { nameof(User.Id), ID };
				yield return new object[] { nameof(User.Name), NAME };
				yield return new object[] { nameof(User.Password), PASSWORD };
				yield return new object[] { nameof(User.RoleId), ROLE };
				yield return new object[] { nameof(User.Email), EMAIL };
			}
		}

		#region Private

		private static readonly Guid ID = Guid.NewGuid();
		private static readonly string NAME = "name01";
		private static readonly string PASSWORD = "password01";
		private static readonly RoleEnum ROLE = RoleEnum.Admin;
		private static readonly string EMAIL = "email01";

		#endregion

	}
}
