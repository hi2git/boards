using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Application.Services {
	public interface IRpcClient<TResponse> {

		//Task<TResponse> Call<TRequest>(TRequest dto);
		TResponse Call<TRequest>(TRequest dto);

	}
}
