using Boards.Users.Domain.Repos;

using Moq;

namespace Boards.Users.Application.Tests.Mocks {
	internal static class UserRepoMock {

		public static Mock<IUserRepo> Create() {
			var mock = new Mock<IUserRepo>();
			return mock;
		}

	}
}
