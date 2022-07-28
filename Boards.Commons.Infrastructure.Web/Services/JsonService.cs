using System;
using System.Linq;
using System.Text.Json;

using Boards.Commons.Application.Services;

namespace Boards.Commons.Infrastructure.Web.Services {
	internal class JsonService : IJsonService {
		public Task<T?> Deserialize<T>(string json, CancellationToken token)  {
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = false };
			return Task.Run(() => JsonSerializer.Deserialize<T>(json, options), token);
		}

		public Task<string> Serialize(object? obj) => Task.Run(() => JsonSerializer.Serialize(obj));
	}
}
