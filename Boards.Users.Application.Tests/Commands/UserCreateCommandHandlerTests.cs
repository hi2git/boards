using Boards.Commons.Domain.DTOs.Users;
using Boards.Domain.Contracts.Users;
using Boards.Users.Application.Commands;
using Boards.Users.Application.Tests.Mocks;
using Boards.Users.Domain.Models;

using Moq;

using NUnit.Framework;

namespace Boards.Users.Application.Tests.Commands {
	public class UserCreateCommandHandlerTests {

		[Test]
		public async Task HandleTest() {
			var hash = "hash01";
			var pwdSvc = PasswordServiceMock.Create(hash: hash);
			var unitOfWork = UnitOfWorkMock.Create();
			var userRepo = UserRepoMock.Create();
			var publish = PublishMock.Create();
			var handler = new UserCreateCommandHandler(pwdSvc.Object, unitOfWork.Object, userRepo.Object, publish.Object);

			var dto = new LoginDTO { Login = "login01", Password = "password01", Email = "email01" };
			var msg = new UserCreateMsg(dto);
			var request = new UserCreateCommand(msg);
			var token = new CancellationToken();
			await handler.Handle(request, token);

			pwdSvc.Verify(n => n.Hash(It.Is<string>(m => m == dto.Password)), Times.Once);
			userRepo.Verify(n => n.Create(It.Is<User>(m => m.Name == dto.Login)), Times.Once);
			unitOfWork.Verify(n => n.Commit(It.IsAny<Func<Task>>()), Times.Once);
		}

	}
}
