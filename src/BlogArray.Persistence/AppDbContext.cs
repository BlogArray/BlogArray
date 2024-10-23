using BlogArray.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<AppRole> AppRoles { get; set; }

    public DbSet<AppOption> AppOptions { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<PostRevision> PostRevisions { get; set; }

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
            entity.HasIndex(b => b.Username).IsUnique();

            entity.HasIndex(b => b.Email).IsUnique();

            entity.HasOne(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.UpdatedUser)
                .WithMany()
                .HasForeignKey(u => u.UpdatedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AppRole>(entity =>
        {
            entity.HasIndex(b => b.NormalizedName).IsUnique();
        });

        builder.Entity<AppOption>(e =>
        {
            e.HasIndex(b => new { b.Key, b.OptionType }).IsUnique();
        });

        builder.Entity<Post>(entity =>
        {
            entity.HasIndex(b => b.Slug).IsUnique();
            entity.Property(e => e.EnableComments).HasDefaultValue(true);
            entity.Property(e => e.DisplayCoverImage).HasDefaultValue(true);
            entity.Property(e => e.EnableSocialSharing).HasDefaultValue(true);
            entity.Property(e => e.DisplayPostTitle).HasDefaultValue(true);
            entity.Property(e => e.EnableContactForm).HasDefaultValue(false);
            entity.Property(e => e.IsFullWidth).HasDefaultValue(false);

            entity.HasOne(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.UpdatedUser)
                .WithMany()
                .HasForeignKey(u => u.UpdatedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PostRevision>(entity =>
        {
            entity.Property(e => e.IsLatest).HasDefaultValue(true);

            entity.HasOne(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Term>(entity =>
        {
            entity.HasIndex(t => new { t.Slug, t.TermType }).IsUnique();
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
            //entity.HasIndex(b => b.Slug).IsUnique();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Seed();
    }
}