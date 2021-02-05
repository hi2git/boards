using System;
using System.Threading.Tasks;

using Boards.Application.Queries.Images;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class ImageController : AbstractApiController {
		private readonly IMediator _mediator;

		public ImageController(IMediator mediator) => _mediator = mediator;

		public async Task<IActionResult> Get([FromQuery] Guid id) {
			var path = await _mediator.Send(new ImagePathGetQuery(id));
			return this.PhysicalFile(path, "image/jpg", $"{id}.jpg");
		}

	}
}
