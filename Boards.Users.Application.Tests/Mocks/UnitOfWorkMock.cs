using Board.Domain.Repos;

using Moq;

namespace Boards.Users.Application.Tests.Mocks {
	internal static class UnitOfWorkMock {

		public static Mock<IUnitOfWork> Create() {
			var mock = new Mock<IUnitOfWork>();
			return mock;
		}

	}
}
