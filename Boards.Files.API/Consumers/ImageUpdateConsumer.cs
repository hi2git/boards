﻿using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Images;
using Boards.Files.Application.Commands;

using MediatR;

namespace Boards.Files.API.Consumers {
	public class ImageUpdateConsumer : AbstractCommandConsumer<ImageUpdateMsg, ImageUpdateResponse> {
		public ImageUpdateConsumer(IMediator mediator, IEventRepo repo, ILogger<ImageUpdateConsumer> log) : base(mediator, repo, log) { }

		protected override Task Handle(ImageUpdateMsg item, CancellationToken token) => this.Mediator.Send(new ImageUpdateCommand(item), token);
	}
}
