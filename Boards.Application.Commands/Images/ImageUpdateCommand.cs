using System;

using Board.Domain.DTO.Images;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Images;

using FluentValidation;


using MediatR;

namespace Boards.Application.Commands.Images {
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

		public ImageUpdateCommandHandler(IClient<ImageUpdateMsg, ImageUpdateResponse> client) : base(client) { }

	}
}
