using Moq;

namespace Boards.Users.Application.Tests.Mocks {
	internal static class PasswordServiceMock {

		public static Mock<IPasswordService> Create(string hash) {
			var mock = new Mock<IPasswordService>();
			mock.Setup(n => n.Hash(It.IsAny<string>())).Returns(hash);
			return mock;
		}

	}
}
