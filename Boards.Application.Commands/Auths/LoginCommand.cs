using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Jwt;
using Boards.Commons.Domain.DTOs.Users;
using Boards.Domain.Contracts.Auths;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.Options;

namespace Boards.Application.Commands.Auths {
	public record LoginCommand : IRequest {

		public LoginCommand(LoginDTO item) => this.Item = item;

		public LoginDTO Item { get; }
	}

	public class LoginCommandValidator : AbstractValidator<LoginCommand> {

		public LoginCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Login).NotEmpty();
			RuleFor(n => n.Item.Password).NotEmpty();
		}

	}

	internal class LoginCommandHandler : IRequestHandler<LoginCommand> {
		private readonly IClient<TokenGetMsg, TokenGetResponse> _client;
		private readonly ICookieService _cookie;
		private readonly AuthSettings _auth;

		public LoginCommandHandler(IClient<TokenGetMsg, TokenGetResponse> client, ICookieService cookie, IOptions<AuthSettings> auth) {
			_client = client;
			_cookie = cookie;
			_auth = auth.Value;
		}

		public async Task<Unit> Handle(LoginCommand request, CancellationToken token) {
			var response = await _client.Send(new TokenGetMsg(request.Item, _auth), token) ;
			_cookie.Add(response.Token);
			return Unit.Value;
		}
	}

}
