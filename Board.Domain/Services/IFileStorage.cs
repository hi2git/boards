using System;
using System.Threading.Tasks;

namespace Board.Domain.Services {
	public interface IFileStorage {

		//Task<string> Read(Guid id);

		Task Write(Guid id, string content);

		Task Delete(Guid id);

		string PathOf(Guid id);
	}
}
