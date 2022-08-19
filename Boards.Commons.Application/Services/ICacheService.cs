using System;
using System.Linq;

namespace Boards.Commons.Application.Services {
	public interface ICacheService {

		//Task<T?> Get<T>(string key, CancellationToken token);

		//Task<T> Set<T>(string key, T value);

		Task<T> GetOrRequest<T>(string key, Func<Task<T>> request, CancellationToken token);

		Task Remove(string key);

		#region Boards

		Task<T> GetOrRequestBoard<T>(Guid id, int filterHash, Func<Task<T>> request, CancellationToken token);

		Task RemoveBoard(Guid id);

		#endregion

	}
}
