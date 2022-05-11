using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Application.Services {
	public interface IProducer {

		public Task Publish<T>(T dto) where T : class;

	}
}
