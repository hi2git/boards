using System;
using System.Linq;

using Board.Domain.Models;

using Boards.Users.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Boards.Posts.Infrastructure {
	public class UsersContext : DbContext {
		public UsersContext(DbContextOptions options) : base(options) => this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<User>(builder => {
				builder.ToTable(typeof(User).Name);
				builder.HasKey(e => e.Id);
				builder.Property(n => n.Name).IsRequired().HasMaxLength(50);
				builder.Property(n => n.Email).IsRequired().HasMaxLength(50);
				builder.Property(n => n.Password).IsRequired();
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
