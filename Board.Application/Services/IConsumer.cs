using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.Services {
	public interface IConsumer<T> where T : class {

		public Task Consume(T dto);

	}
}
