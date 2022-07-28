using System;
using System.Linq;

namespace Boards.Commons.Application.Services {
	public interface IJsonService{

		Task<T?> Deserialize<T> (string json, CancellationToken token);

		Task<string> Serialize(object? obj);

	}
}
