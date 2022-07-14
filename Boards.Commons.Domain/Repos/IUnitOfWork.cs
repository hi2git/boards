using System;
using System.Threading.Tasks;

namespace Board.Domain.Repos {
	public interface IUnitOfWork {
		Task Commit(Func<Task>? action = null);
	}
}
