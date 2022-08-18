using System;
using System.Collections.Generic;

using Board.Domain.Repos;

using Boards.Posts.Domain.Models;

namespace Boards.Posts.Domain.Repos {
	public interface IPostRepo : IRepo<Post> {

		Task<List<Post>> GetAll(Guid boardId, CancellationToken token);

		IQueryable<Post> Query(Guid boardId);

	}
}
