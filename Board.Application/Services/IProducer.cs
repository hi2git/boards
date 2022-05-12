using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Application.Services {
	public interface IProducer {

		Task Publish<T>(T dto) where T : class;
		Task Publish<T>(T dto, string id, string queue) where T : class;

	}
}
