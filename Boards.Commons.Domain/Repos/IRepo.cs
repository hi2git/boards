﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Domain.Models;

namespace Board.Domain.Repos {
	public interface IRepo<T> where T : Entity<Guid> {

		/// <summary>Возвращает список всех сущностей</summary>
		/// <returns>список всех сущностей</returns>
		Task<List<T>> GetAll(CancellationToken token);

		/// <summary>Выполняет асинхронное получение сущности по идентификатору</summary>
		/// <returns>искомую сущность или null</returns>
		Task<T> Get(Guid id, CancellationToken token);

		/// <summary>обновляет сущность</summary>
		/// <param name="entity">сущность для сохранения</param>
		Task Update(T entity);

		/// <summary>Добавляет сущность</summary>
		/// <param name="entity">сущность для сохранения</param>
		Task Create(T entity);

		/// <summary>Удаляет сущность из репозитория</summary>
		/// <param name="entity">сущность для удаления</param>
		Task Delete(T entity);

	}
}
