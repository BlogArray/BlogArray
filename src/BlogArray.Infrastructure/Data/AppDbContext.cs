using BlogArray.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, IdentityRole<int>, int>(options)
{
    public DbSet<AppOption> AppOptions { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Storage> Storages { get; set; }

    public DbSet<Statistic> Statistics { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<PostCategory> PostCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppOption>(e =>
        {
            e.HasIndex(b => new { b.Key, b.OptionType }).IsUnique();
        });

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

        builder.Entity<Post>(e =>
        {
            e.HasIndex(b => b.Slug).IsUnique();
            e.Property(e => e.AllowComments).HasDefaultValue(true);
            e.Property(e => e.ShowCover).HasDefaultValue(true);
            e.Property(e => e.ShowSharingIcon).HasDefaultValue(true);
            e.Property(e => e.ShowHeading).HasDefaultValue(true);
        });

        builder.Entity<Category>(e =>
        {
            e.HasIndex(b => b.Slug).IsUnique();
        });

        builder.Entity<PostCategory>(e =>
        {
            e.Property(e => e.Order).HasDefaultValue(0);

            e.HasKey(t => new { t.PostId, t.CategoryId });

            e.HasIndex(t => new { t.PostId, t.CategoryId }).IsUnique();

            e.HasOne(pc => pc.Post)
            .WithMany(p => p.Categories)
            .HasForeignKey(pc => pc.PostId);

            e.HasOne(pc => pc.Category)
            .WithMany(c => c.PostCategories)
            .HasForeignKey(pc => pc.CategoryId);
        });

        builder.Seed();
    }
}