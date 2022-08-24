using System;
using System.Linq;

using Boards.Commons.Infrastructure.Repos;

namespace Boards.Posts.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(PostsContext context) : base(context) { }

	}
}
