using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards.Commons.Application.Utils {
	public static class CollectionExt {

		public static async Task ForEachAsync<T>(this IEnumerable<T> items, Func<T, Task> func) {
			items = items ?? throw new ArgumentNullException(nameof(items));
			foreach (var item in items) {
				await func(item);
			}
		}

		public static Task WhenAll<T>(this IEnumerable<T> items, Func<T, Task> func) { 
			items = items ?? throw new ArgumentNullException(nameof(items));
			var tasks = items.Select(func);
			return Task.WhenAll(tasks);
		}

	}
}
