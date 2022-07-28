using System;
using System.Linq;

using Boards.Commons.Application.Services;

using Microsoft.Extensions.Caching.Distributed;

namespace Boards.Commons.Infrastructure.Web.Services {
	internal class CacheService : ICacheService {
		private readonly IDistributedCache _cache;
		private readonly IJsonService _json;

		public CacheService(IDistributedCache cache, IJsonService json) {
			_cache = cache;
			_json = json;
		}

		public async Task<T> GetOrRequest<T>(string key, Func<CancellationToken, Task<T>> request, CancellationToken token) => 
			await this.Get<T>(key, token) ?? await this.Request(key, request, token);

		public Task Remove(string key) => _cache.RemoveAsync(key);

		#region Private

		private async Task<T?> Get<T>(string key, CancellationToken token) {
			var content = await _cache.GetStringAsync(key, token);
			return content is null ? default : await _json.Deserialize<T>(content, token);
		}

		private async Task<T> Set<T>(string key, T value) {
			var content = await _json.Serialize(value);
			await _cache.SetStringAsync(key, content);
			return value;
		}

		private async Task<T> Request<T>(string key, Func<CancellationToken, Task<T>> request, CancellationToken token) => await this.Set(key, await request(token));

		#endregion
	}
}
