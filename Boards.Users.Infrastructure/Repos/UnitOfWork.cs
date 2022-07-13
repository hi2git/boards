using System;
using System.Linq;

using Boards.Common.Infrastructure.Repos;
using Boards.Posts.Infrastructure;

namespace Boards.Users.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(UsersContext context) : base(context) { }

	}
}
