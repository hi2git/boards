﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
using Boards.Commons.Domain.DTOs.Users;
using Boards.Domain.Contracts.Users;
using Boards.Front.Application.Commands.Auths;

using System.Text.Json;

using FluentValidation;

using MediatR;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Boards.Commons.Application.Services;

namespace Boards.Front.Application.Commands.Users {
	public record UserCreateCommand : UserCreateMsg, IRequest {

		public UserCreateCommand(LoginDTO item) : base(item) { }

	}

	public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand> {

		public UserCreateCommandValidator(IHttpClientFactory http, IOptions<AppSettings> app) {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Password).NotEmpty().MaximumLength(50);
			RuleFor(n => n.Item.Email).NotEmpty().MaximumLength(50).EmailAddress();
			RuleFor(n => n.Item.Login).NotEmpty().MaximumLength(50);

			var client = http.CreateClient("Captcha");
			RuleFor(n => n.Item.Captcha).MustAsync(async (n, token) => {
				var path = $"?secret={app.Value.GooglePrivateKey}&response={n}";
				var response = await client.PostAsync(path, null, token);
				if (!response.IsSuccessStatusCode) {
					return false;
				}

				var result = await JsonSerializer.DeserializeAsync<CaptchaResponse>(response.Content.ReadAsStream(token), cancellationToken: token);	// TODO: use IJsonService
				return result.success;
			});


		}

		private record CaptchaResponse {
			public CaptchaResponse() { }

			public bool success { get; set; }

		}

	}

	internal class UserCreateCommandHandler : AbstractHandler<UserCreateCommand, UserCreateMsg, UserCreateResponse> {
		private readonly IMediator _mediator;

		public UserCreateCommandHandler(IClient<UserCreateMsg, UserCreateResponse> client, IMediator mediator/*, ICacheService cache*/) : base(client/*, cache*/) => _mediator = mediator;

		protected override Task HandleResponse(UserCreateResponse response, UserCreateCommand request, CancellationToken token) => _mediator.Send(new LoginCommand(request.Item), token);

		//protected override string CacheKey(UserCreateCommand _) => "all_users";

	}
}
