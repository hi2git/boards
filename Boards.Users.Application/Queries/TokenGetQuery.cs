using System;
using System.Linq;

using Board.Domain.DTO.Jwt;

using Boards.Domain.Contracts.Auths;

using FluentValidation;

using MediatR;

namespace Boards.Users.Application.Queries {
	public record TokenGetQuery : TokenGetMsg, IRequest<JwtTokenDTO> {

		public TokenGetQuery(TokenGetMsg msg) : base(msg.Item, msg.Auth) { }
	}

	public class TokenGetQueryValidator : AbstractValidator<TokenGetQuery> {

		public TokenGetQueryValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Login).NotEmpty();
			RuleFor(n => n.Item.Password).NotEmpty();

			RuleFor(n => n.Auth).NotEmpty();
			RuleFor(n => n.Auth.Issuer).NotEmpty();
			RuleFor(n => n.Auth.Audience).NotEmpty();
			RuleFor(n => n.Auth.Secret).NotEmpty();
			RuleFor(n => n.Auth.Lifetime).GreaterThan(0);

		}

	}

	internal class TokenGetQueryHandler : IRequestHandler<TokenGetQuery, JwtTokenDTO> {
		private readonly IAuthService _authSvc;

		public TokenGetQueryHandler(IAuthService authSvc) => _authSvc = authSvc;

		public Task<JwtTokenDTO> Handle(TokenGetQuery request, CancellationToken token) => _authSvc.Login(request.Item, request.Auth, token);
	}
}
