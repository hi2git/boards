using System;
using System.Linq;

using Board.Infrastructure.Repository.Implementation;

namespace Boards.Posts.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(PostsContext context) : base(context) { }

	}
}
