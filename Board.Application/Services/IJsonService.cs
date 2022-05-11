using System;
using System.Collections.Generic;
using System.Linq;

namespace Board.Application.Services {
	public interface IJsonService {

		string Serialize(object dto);

		T Deserialize<T>(string text);

	}
}
