using System;

namespace Boards.Domain.Models {

	public abstract class Entity : Entity<Guid> {
		protected Entity() { }

		protected Entity(Guid id) : base(id) { }
	}

	public abstract class Entity<T> {

		protected Entity() { }

		protected Entity(T id) => this.Id = id;

		public T Id { get; protected set; }
	}

	public abstract class Nameable : Nameable<Guid> {

		protected Nameable() { }

		protected Nameable(Guid id, string name) : base(id, name) { }
	}

	public abstract class Nameable<T> : Entity<T> {
		protected Nameable() { }

		protected Nameable(T id, string name) : base(id) => this.Name = name;

		public string Name { get; set; }

	}
}
