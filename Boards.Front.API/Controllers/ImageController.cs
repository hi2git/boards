using System;
using System.Threading;
using System.Threading.Tasks;

using Boards.Application.Queries.Images;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class ImageController : AbstractApiController {
		private readonly IMediator _mediator;

		public ImageController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] Guid id, CancellationToken token) {
			//var path = await _mediator.Send(new ImagePathGetQuery(id), token);
			//return this.PhysicalFile(path, "image/jpg", $"{id}.jpg");
			var content = await _mediator.Send(new ImageGetQuery(id), token);
			//return $"data:image/jpg;base64, {content}";
			var bytes = Convert.FromBase64String(content);
			return this.File(bytes, "image/jpg", $"{id}.jpg", true);
		}

	}
}
