﻿using System;
using System.Linq;

using Boards.Posts.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Boards.Posts.Infrastructure {
	public class PostsContext : DbContext {
		public PostsContext(DbContextOptions options) : base(options) => this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Post>(builder => {
				builder.ToTable(typeof(Post).Name);
				builder.HasKey(e => e.Id);
				builder.Property(n => n.OrderNumber).IsRequired();
				builder.Property(n => n.Description);
				builder.Property(n => n.IsDone).IsRequired();
			});

		}
	}
}
