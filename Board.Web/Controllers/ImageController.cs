using System;
using System.Threading.Tasks;

using Board.Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class ImageController : AbstractApiController {
		private readonly IFileStorage _fileStorage;

		public ImageController(IFileStorage fileStorage) => _fileStorage = fileStorage;

		public async Task<IActionResult> Get([FromQuery] Guid id) {
			var path = _fileStorage.PathOf(id);
			return this.PhysicalFile(path, "image/jpg", $"{id}.jpg");
		}

	}
}
