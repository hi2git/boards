using System;
using System.Linq;

using Board.Infrastructure.Repository.Implementation;

using Boards.Posts.Domain.Models;
using Boards.Posts.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Boards.Posts.Infrastructure.Repos {
	internal class PostRepo : AbstractRepo<Post>, IPostRepo {
		public PostRepo(PostsContext context) : base(context) { }

		public Task<List<Post>> GetAll(Guid boardId, CancellationToken token) => this.Query
			.Where(n => n.BoardId == boardId)
			.OrderByDescending(n => n.OrderNumber)
			.ToListAsync(token);
	}
}
