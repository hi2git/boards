using System;
using System.Linq;

using Boards.Commons.Infrastructure.Repos;
using Boards.Posts.Domain.Models;
using Boards.Posts.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Boards.Posts.Infrastructure.Repos {
	internal class PostRepo : AbstractRepo<Post>, IPostRepo {
		public PostRepo(PostsContext context) : base(context) { }

		public Task<List<Post>> GetAll(Guid boardId, CancellationToken token) => this.Query(boardId)
			//.Where(n => n.BoardId == boardId)
			//.OrderByDescending(n => n.OrderNumber)
			.ToListAsync(token);

		public IQueryable<Post> Query(Guid boardId) => base.Query
			.Where(n => n.BoardId == boardId)
			.OrderByDescending(n => n.OrderNumber);

	}
}
