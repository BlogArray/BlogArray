using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, IdentityRole<int>, int>(options)
{
	public DbSet<AppOption> AppOptions { get; set; }

	public DbSet<Post> Posts { get; set; }

	public DbSet<Term> Terms { get; set; }

	public DbSet<PostTerm> PostTerms { get; set; }

	public DbSet<Comment> Comments { get; set; }

	public DbSet<Storage> Storages { get; set; }

	public DbSet<Statistic> Statistics { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<AppUser>(entity =>
		{
			entity.HasOne(u => u.CreatedUser)
				.WithMany()
				.HasForeignKey(u => u.CreatedUserId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(u => u.UpdatedUser)
				.WithMany()
				.HasForeignKey(u => u.UpdatedUserId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		builder.Entity<AppOption>(e =>
		{
			e.HasIndex(b => new { b.Key, b.OptionType }).IsUnique();
		});

		builder.Entity<Post>(entity =>
		{
			entity.HasIndex(b => b.Slug).IsUnique();
			entity.Property(b => b.PostStatus).HasDefaultValue(PostStatus.Published);
			entity.Property(e => e.AllowComments).HasDefaultValue(true);
			entity.Property(e => e.ShowCover).HasDefaultValue(true);
			entity.Property(e => e.ShowSharingIcon).HasDefaultValue(true);
			entity.Property(e => e.ShowHeading).HasDefaultValue(true);
			entity.Property(e => e.ShowContactPage).HasDefaultValue(false);
			entity.Property(e => e.IsWidePage).HasDefaultValue(false);

			entity.HasOne(e => e.Parent)
				.WithMany()
				.HasForeignKey(e => e.ParentId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(u => u.CreatedUser)
				.WithMany()
				.HasForeignKey(u => u.CreatedUserId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(u => u.UpdatedUser)
				.WithMany()
				.HasForeignKey(u => u.UpdatedUserId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		builder.Entity<Term>(entity =>
		{
			entity.HasIndex(b => b.Slug).IsUnique();
		});

		builder.Entity<PostTerm>(entity =>
		{
			entity.Property(e => e.Order).HasDefaultValue(0);

			entity.HasKey(t => new { t.PostId, t.TermId });

			entity.HasIndex(t => new { t.PostId, t.TermId }).IsUnique();

			entity.HasOne(pc => pc.Post)
			.WithMany(p => p.Terms)
			.HasForeignKey(pc => pc.PostId);

			entity.HasOne(pc => pc.Term)
			.WithMany(c => c.PostTerms)
			.HasForeignKey(pc => pc.TermId);
		});

		builder.Entity<Comment>(entity =>
		{
			entity.Property(e => e.Status).HasDefaultValue(CommentStatus.Published);

			entity.HasOne(u => u.CreatedUser)
				.WithMany()
				.HasForeignKey(u => u.CreatedUserId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(u => u.UpdatedUser)
				.WithMany()
				.HasForeignKey(u => u.UpdatedUserId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		builder.Entity<Storage>(entity =>
		{
			entity.HasIndex(b => b.Slug).IsUnique();
			entity.Property(e => e.IsDeleted).HasDefaultValue(false);

			entity.HasOne(u => u.CreatedUser)
				.WithMany()
				.HasForeignKey(u => u.CreatedUserId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		builder.Seed();
	}
}