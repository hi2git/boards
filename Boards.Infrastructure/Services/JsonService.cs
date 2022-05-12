using System;
using System.Collections.Generic;
using System.Linq;

using Board.Application.Services;

using Newtonsoft.Json;

namespace Boards.Infrastructure.Services {
	internal class JsonService : IJsonService {
		public T Deserialize<T>(string text) => JsonConvert.DeserializeObject<T>(text);

		public string Serialize(object dto) => JsonConvert.SerializeObject(dto);
	}
}
