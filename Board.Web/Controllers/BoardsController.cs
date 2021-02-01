using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.DTO;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardsController : AbstractApiController {


		public BoardsController() {

		}

		[HttpGet]
		public Task<List<IdNameDTO>> GetAll() => Task.FromResult(new List<IdNameDTO> {
			 new IdNameDTO { Id = Guid.NewGuid(), Name = "Arbol111" },
			  new IdNameDTO { Id = Guid.NewGuid(), Name = "Mouse" }
		});

	}
}
