using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Images;
using Boards.Files.Application.Commands;

using MediatR;

namespace Boards.Files.API.Consumers {
	public class ImageGetConsumer : AbstractQueryConsumer<ImageGetMsg, ImageGetResponse> {

		public ImageGetConsumer(IMediator mediator, ILogger<ImageGetConsumer> log) : base(mediator, log) { }

		protected override async Task<ImageGetResponse> Handle(ImageGetMsg item, CancellationToken token) {
			var content = await this.Mediator.Send(new ImageGetQuery(item), token);
			return new ImageGetResponse() { Content = content };
		}

	}
}
