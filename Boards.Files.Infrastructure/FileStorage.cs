using System;

using Boards.Files.Application;

using Microsoft.Extensions.Options;

namespace Boards.Files.Infrastructure {
	public class FileStorage : IFileStorage {
		private readonly AppSettings _appSettings;

		public FileStorage(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;

		public async Task<string> Get(Guid id, CancellationToken token) {
			var path = this.PathOf(id);
			var bytes = await File.ReadAllBytesAsync(path, token);
			return Convert.ToBase64String(bytes);
		}

		public Task Write(Guid id, string base64) {
			var bytes = Convert.FromBase64String(base64);
			var path = this.PathOf(id);
			return File.WriteAllBytesAsync(path, bytes);
		}

		public Task Delete(Guid id) => Task.Run(() => File.Delete(this.PathOf(id)));

		private string PathOf(Guid id) => Path.Combine(_appSettings.BoardPath, $"{id}.jpg");
	}
}
