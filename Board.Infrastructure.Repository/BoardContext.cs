using System;
using System.Diagnostics.CodeAnalysis;

using Board.Domain.Enums;
using Board.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository {
	public class BoardContext : DbContext {
		public BoardContext([NotNull] DbContextOptions options) : base(options) => this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

		#region Props

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<BoardItem> BoardItems { get; set; }

		#endregion

		#region Configuration

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			if (modelBuilder == null)
				return;

			modelBuilder.Entity<User>(builder => {
				builder.ToTable(typeof(User).Name);
				builder.HasKey(e => e.Id);
				builder.Property(n => n.Name).IsRequired().HasMaxLength(50);
				builder.Property(n => n.Password).IsRequired();

				builder.HasOne(n => n.Role)
					.WithMany(n => n.Users)
					.HasForeignKey(nameof => nameof.RoleId);
			});

			modelBuilder.Entity<BoardItem>(builder => {
				builder.ToTable(typeof(BoardItem).Name);
				builder.HasKey(e => e.Id);
				builder.Property(n => n.OrderNumber);
				builder.Property(n => n.Description);
				builder.Property(n => n.IsDone).IsRequired();

				builder.HasOne(n => n.User)
					.WithMany(n => n.BoardItems)
					.HasForeignKey(n => n.UserId);
			});

			modelBuilder.Entity<Role>(builder => {
				builder.ToTable(typeof(Role).Name);
				builder.HasKey(e => e.Id);

				builder.Property(e => e.Id).HasColumnType("smallint").HasConversion(n => (short)n, n => (RoleEnum)n);
				builder.Property(n => n.Name).IsRequired().HasMaxLength(50);
			});
		}

		#endregion
	}
}
