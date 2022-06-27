using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Board.Domain.Repos {
	public interface IEntityRepo<T> where T : class {

		/// <summary>Выполняет асинхронный запрос всех элементов текущей сущности</summary>
		/// <returns>список всех элементов</returns>
		IQueryable<T> Query(Expression<Func<T, bool>> predicate = null);

		/// <summary>Выполняет асинхронное получение сущности по идентификатору</summary>
		/// <returns>искомую сущность или null</returns>
		Task<T> Get(Guid id, CancellationToken token);

		/// <summary>Добавляет сущность</summary>
		/// <param name="entity">сущность для сохранения</param>
		Task Create(T entity);

		/// <summary>Удаляет сущность из репозитория</summary>
		/// <param name="entity">сущность для удаления</param>
		Task Delete(T entity);

		/// <summary>обновляет сущность</summary>
		/// <param name="entity">сущность для сохранения</param>
		Task Update(T entity);

		/// <summary>Возвращает список всех элементов</summary>
		/// <returns>список всех элементов</returns>
		Task<List<T>> GetAll(CancellationToken token);

		///// <summary>Возвращает количество элементов, соответствующих заданному условию</summary>
		///// <param name="predicate">функция условия</param>
		///// <returns>количество элементов, соответствующих заданному условию</returns>
		//Task<int> Count(Expression<Func<T, bool>> predicate = null);

		//Task UpdateManyToMany<TKey>(IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, TKey> getKey);
	}
}
