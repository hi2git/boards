using System;

using Board.Domain.Constants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = RoleNames.User)]
	public abstract class AbstractApiController : ControllerBase { }
}
