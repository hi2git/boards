using System;
using System.Linq;

namespace Boards.Commons.Application.Services {
	public interface ICacheService {

		//Task<T?> Get<T>(string key, CancellationToken token);

		//Task<T> Set<T>(string key, T value);

		Task<T> GetOrRequest<T>(string key, Func<CancellationToken, Task<T>> request, CancellationToken token);

		Task Remove(string key);

	}
}
