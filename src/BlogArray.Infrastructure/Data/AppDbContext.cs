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

    public DbSet<Term> Terms { get; set; }

    public DbSet<PostTerm> PostTerms { get; set; }

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

        builder.Entity<Term>(e =>
        {
            e.HasIndex(b => b.Slug).IsUnique();
        });

        builder.Entity<PostTerm>(e =>
        {
            e.Property(e => e.Order).HasDefaultValue(0);

            e.HasKey(t => new { t.PostId, t.TermId });

            e.HasIndex(t => new { t.PostId, t.TermId }).IsUnique();

            e.HasOne(pc => pc.Post)
            .WithMany(p => p.Terms)
            .HasForeignKey(pc => pc.PostId);

            e.HasOne(pc => pc.Term)
            .WithMany(c => c.PostTerms)
            .HasForeignKey(pc => pc.TermId);
        });

        builder.Seed();
    }
}