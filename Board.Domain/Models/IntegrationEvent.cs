using System;
using System.Linq;

namespace Board.Domain.Models {
	public class IntegrationEvent : Nameable {

		// EF only
		protected IntegrationEvent() {}

		public IntegrationEvent(Guid id, string name, DateTime date) : base(id, name) => this.Date = date != default ? date : throw new ArgumentNullException(nameof(date));

		#region Properties

		public DateTime Date { get; protected set; }

		#endregion

	}
}
