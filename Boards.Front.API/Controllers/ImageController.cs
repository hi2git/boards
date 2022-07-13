using System;
using System.Threading;
using System.Threading.Tasks;

using Boards.Application.Commands.Images;
using Boards.Application.Queries.Images;
using Boards.Commons.Domain.DTOs.Images;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class ImageController : AbstractApiController {
		private readonly IMediator _mediator;

		public ImageController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] Guid id, CancellationToken token) {
			var content = await _mediator.Send(new ImageGetQuery(id), token);
			var bytes = Convert.FromBase64String(content);
			return this.File(bytes, "image/jpg", $"{id}.jpg", true);
		}

		[HttpPut]
		public Task Update([FromBody] ImageDTO dto) => _mediator.Send(new ImageUpdateCommand(dto));

	}
}
