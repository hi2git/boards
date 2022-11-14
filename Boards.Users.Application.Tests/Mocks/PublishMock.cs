using MassTransit;

using Moq;

namespace Boards.Users.Application.Tests.Mocks {
	internal static class PublishMock {

		public static Mock<IPublishEndpoint> Create() {
			var mock = new Mock<IPublishEndpoint>();
			return mock;
		}

	}
}
