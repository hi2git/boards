using System;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Images;
using Boards.Domain.Contracts.Images;

using FluentValidation;


using MediatR;

namespace Boards.Front.Application.Commands.Images {
	public record ImageUpdateCommand : ImageUpdateMsg, IRequest {

		public ImageUpdateCommand(ImageDTO item) : base(item.Id, item.Content) { }

	}

	public class ImageUpdateCommandValidator : AbstractValidator<ImageUpdateCommand> {

		public ImageUpdateCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Content).NotEmpty();
		}
	}

	internal class ImageUpdateCommandHandler : AbstractHandler<ImageUpdateCommand, ImageUpdateMsg, ImageUpdateResponse> {

		public ImageUpdateCommandHandler(IClient<ImageUpdateMsg, ImageUpdateResponse> client, ICacheService cache) : base(client, cache) { }

		protected override string CacheKey => $"post_{111}";
	}
}
