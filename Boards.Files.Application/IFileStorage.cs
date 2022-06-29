using System;

namespace Boards.Files.Application {
	public interface IFileStorage {

		Task<string> Get(Guid id, CancellationToken token);

		Task Write(Guid id, string content);

		Task Delete(Guid id);
		
		//string PathOf(Guid id);
	}
}
