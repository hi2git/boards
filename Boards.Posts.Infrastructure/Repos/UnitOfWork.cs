using System;
using System.Linq;

using Board.Infrastructure.Repository;

namespace Boards.Posts.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(PostsContext context) : base(context) { }

	}
}
