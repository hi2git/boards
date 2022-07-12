using System;
using System.Linq;

using Board.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Boards.Boards.Infrastructure {
	public class BoardsContext : DbContext {
		public BoardsContext(DbContextOptions options) : base(options) => this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Domain.Models.Board>(builder => {
				builder.ToTable(typeof(Domain.Models.Board).Name);
				builder.HasKey(e => e.Id);
				builder.Property(n => n.Name).IsRequired().HasMaxLength(50);
				builder.Property(n => n.UserId).IsRequired();
			});

			modelBuilder.Entity<IntegrationEvent>(builder => {
				builder.ToTable(typeof(IntegrationEvent).Name);
				builder.HasKey(e => e.Id);
				builder.Property(n => n.Name).IsRequired();
				builder.Property(n => n.Date).IsRequired();
			});

		}
	}
}
