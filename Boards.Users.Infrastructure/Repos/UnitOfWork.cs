using System;
using System.Linq;

using Board.Infrastructure.Repository;

using Boards.Posts.Infrastructure;

namespace Boards.Users.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(UsersContext context) : base(context) { }

	}
}
