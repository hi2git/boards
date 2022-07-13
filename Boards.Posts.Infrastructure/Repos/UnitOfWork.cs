using System;
using System.Linq;

using Boards.Common.Infrastructure.Repos;

namespace Boards.Posts.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(PostsContext context) : base(context) { }

	}
}
