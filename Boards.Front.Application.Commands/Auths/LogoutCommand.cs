using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application.Services;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Commands.Auths {
	public record LogoutCommand : IRequest { }

	public class LogoutCommandValidator : AbstractValidator<LogoutCommand> { }

	internal class LogoutCommandHandler : IRequestHandler<LogoutCommand> {
		private readonly ICookieService _cookie;

		public LogoutCommandHandler(ICookieService cookie) => _cookie = cookie;

		public Task<Unit> Handle(LogoutCommand request, CancellationToken token) {
			_cookie.Remove();
			return Unit.Task;
		}
	}
}
