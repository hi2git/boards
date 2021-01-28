using System;
using System.IO;
using System.Threading.Tasks;

using Board.Application;
using Board.Domain.Services;

using Microsoft.Extensions.Options;

namespace Board.Infrastructure.Files {
	public class FileStorage : IFileStorage {
		private readonly AppSettings _appSettings;

		public FileStorage(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;

		public Task Write(Guid id, string base64) {
			var bytes = Convert.FromBase64String(base64);
			return File.WriteAllBytesAsync(PathOf(id), bytes);
		}

		public Task Delete(Guid id) => Task.Run(() => File.Delete(PathOf(id)));

		//public Task<string> Read(Guid id) => File.ReadAllTextAsync(PathOf(id));

		public string PathOf(Guid id) => Path.Combine(_appSettings.BoardPath, id.ToString());
	}
}
