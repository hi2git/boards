using System;
using System.Linq;

using Boards.Commons.Infrastructure.Repos;

namespace Boards.Boards.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(BoardsContext context) : base(context) { }

	}
}
