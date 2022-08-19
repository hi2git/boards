using System;
using System.Linq;

using Boards.Commons.Application.Services;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Distributed;

namespace Boards.Commons.Infrastructure.Web.Services {
	internal class CacheService : ICacheService { 
		private const string BOARDS_KEYS = "BOARDS_KEYS";


		private readonly IDistributedCache _cache;
		private readonly IJsonService _json;

		public CacheService(IDistributedCache cache, IJsonService json) {
			_cache = cache;
			_json = json;
		}

		public async Task<T> GetOrRequest<T>(string key, Func<Task<T>> request, CancellationToken token) =>
			await this.Get<T>(key, token) ?? await this.Request(key, request);

		public  Task Remove(string key) =>_cache.RemoveAsync(key);

		#region Boards

		public async Task<T> GetOrRequestBoard<T>(Guid id, int filterHash, Func<Task<T>> request, CancellationToken token) =>
			await this.Get<T>($"board_{id}_{filterHash}", token) ?? await this.RequestBoard(id, filterHash, request);

		public async Task RemoveBoard(Guid id) {
			var hashes = await this.Get<List<string>>($"{BOARDS_KEYS}_{id}", default);
			var tasks = hashes?.Select(n => this.Remove($"board_{id}_{n}")) ?? Enumerable.Empty<Task>();
			await Task.WhenAll(tasks);
			await this.Remove($"{BOARDS_KEYS}_{id}");
		}

		private async Task<T> RequestBoard<T>(Guid id, int hash, Func<Task<T>> request) => await this.SetBoard(id, hash, await request());  // TODO: count cache misses

		private async Task<T> SetBoard<T>(Guid id, int hash, T value) {
			var content = await _json.Serialize(value);

			var keys = await this.Get<List<string>>($"{BOARDS_KEYS}_{id}", default) ?? new List<string>();
			keys.Add($"{hash}");
			await this.Set($"{BOARDS_KEYS}_{id}", keys);

			await _cache.SetStringAsync($"board_{id}_{hash}", content);

			return value;
		}

		#endregion

		#region Private

		private async Task<T?> Get<T>(string key, CancellationToken token) {
			var content = await _cache.GetStringAsync(key, token);
			return content is null ? default : await _json.Deserialize<T>(content, token);
		}

		private async Task<T> Set<T>(string key, T value) {
			var content = await _json.Serialize(value);
			//if (key.StartsWith("board_")) {
			//	var keys = await this.Get<List<string>>(BOARDS_KEYS, default) ?? new List<string>();
			//	keys.Add(key);
			//	await this.Set(BOARDS_KEYS, keys);
			//}
			await _cache.SetStringAsync(key, content);
			
			return value;
		}

		private async Task<T> Request<T>(string key, Func<Task<T>> request) => await this.Set(key, await request());	// TODO: count cache misses

		#endregion
	}
}
