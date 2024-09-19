﻿// <auto-generated />
using System;
using BlogArray.Persistence.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogArray.Persistence.Sqlite.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20240919180240_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("BlogArray.Domain.Entities.AppOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AutoLoad")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<int>("OptionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Key", "OptionType")
                        .IsUnique();

                    b.ToTable("AppOptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AutoLoad = true,
                            Key = "SiteInfo",
                            OptionType = 0,
                            Value = "{\"Title\":\"Bloggery\",\"Tagline\":\"A robust blogging platform.\",\"Description\":\"It is a robust blogging platform that offers a wide range of features.\",\"Icon\":\"/content/images/logo.svg\",\"Logo\":\"/content/images/logo.svg\"}"
                        },
                        new
                        {
                            Id = 2,
                            AutoLoad = true,
                            Key = "SMTP",
                            OptionType = 0,
                            Value = "{\"Username\":\"localhost\",\"Password\":\"password\",\"Host\":\"localhost\",\"Port\":587,\"UseSSL\":false}"
                        },
                        new
                        {
                            Id = 3,
                            AutoLoad = true,
                            Key = "PageOptions",
                            OptionType = 0,
                            Value = "{\"HomePage\":\"posts\",\"StaticHomePage\":\"home\",\"DefaultCategory\":1,\"DefaultCover\":\"/content/images/page-image.webp\",\"ItemsPerPage\":10,\"SearchEngineVisibility\":true}"
                        },
                        new
                        {
                            Id = 4,
                            AutoLoad = true,
                            Key = "Menu:TopNav",
                            OptionType = 1,
                            Value = "[{\"Page\":\"Home\",\"Link\":\"/\",\"SubLinks\":[]},{\"Page\":\"Sri Ramalaya temple\",\"Link\":\"/page/ramalayam-temple\",\"SubLinks\":[]},{\"Page\":\"Contribute\",\"Link\":\"/page/contribute\",\"SubLinks\":[]},{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"Blog\",\"Link\":\"/blog/list\",\"SubLinks\":[]},{\"Page\":\"About\",\"Link\":\"/page/about\",\"SubLinks\":[]},{\"Page\":\"Contact\",\"Link\":\"/contact\",\"SubLinks\":[]}]"
                        },
                        new
                        {
                            Id = 5,
                            AutoLoad = true,
                            Key = "Menu:FooterLinks",
                            OptionType = 1,
                            Value = "[{\"Page\":\"Home\",\"Link\":\"/\",\"SubLinks\":[]},{\"Page\":\"Sri Ramalaya temple\",\"Link\":\"/page/ramalayam-temple\",\"SubLinks\":[]},{\"Page\":\"Contribute\",\"Link\":\"/page/contribute\",\"SubLinks\":[]},{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"About\",\"Link\":\"/page/about\",\"SubLinks\":[]},{\"Page\":\"Contact\",\"Link\":\"/contact\",\"SubLinks\":[]},{\"Page\":\"Privacy Policy\",\"Link\":\"/page/privacy-policy\",\"SubLinks\":[]}]"
                        },
                        new
                        {
                            Id = 6,
                            AutoLoad = true,
                            Key = "Menu:QuickLinks",
                            OptionType = 1,
                            Value = "[{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"Blog\",\"Link\":\"/blog/list\",\"SubLinks\":[]}]"
                        },
                        new
                        {
                            Id = 7,
                            AutoLoad = true,
                            Key = "Menus",
                            OptionType = 0,
                            Value = "{\"HeaderMenu\":null,\"HeaderMenu2\":null,\"HeaderMenu3\":null,\"SocialMenu\":null,\"SocialMenu2\":null,\"FooterMenu\":null,\"FooterMenu2\":null,\"FooterMenu3\":null,\"FooterMenu4\":null}"
                        });
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique();

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Editor",
                            NormalizedName = "EDITOR"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Author",
                            NormalizedName = "AUTHOR"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Subscriber",
                            NormalizedName = "SUBSCRIBER"
                        });
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedUserId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            CreatedOn = new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893),
                            DisplayName = "Admin",
                            Email = "admin@vtp.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            PasswordHash = "$2a$11$xO.YQRxXr4KYzralfcHauuSmpiPpJy.YRQpJTt4QTZJsMauYIi3Ca",
                            RoleId = 1,
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            CreatedOn = new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893),
                            Email = "anonymous@vtp.com",
                            EmailConfirmed = false,
                            LockoutEnabled = true,
                            RoleId = 4,
                            TwoFactorEnabled = false,
                            UserName = "anonymous"
                        });
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorIP")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorSite")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ParsedContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RawContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserAgent")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AllowComments")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<int>("CommentsCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cover")
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWidePage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<int>("PostStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PostType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("PublishedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ShowAuthor")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowContactPage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("ShowCover")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<bool>("ShowHeading")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<bool>("ShowSharingIcon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.PostRevision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EditorType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLatest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("ParsedContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RawContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("PostId");

                    b.ToTable("PostRevisions");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.PostTerm", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("PostId", "TermId");

                    b.HasIndex("TermId");

                    b.HasIndex("PostId", "TermId")
                        .IsUnique();

                    b.ToTable("PostTerms");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Statistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserAgent")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ViewedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AssetType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<long>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("TEXT");

                    b.Property<int>("TermType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Terms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "",
                            Name = "Uncategorized",
                            Slug = "uncategorized",
                            TermType = 0
                        });
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.AppUser", b =>
                {
                    b.HasOne("BlogArray.Domain.Entities.AppUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BlogArray.Domain.Entities.AppRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogArray.Domain.Entities.AppUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedUser");

                    b.Navigation("Role");

                    b.Navigation("UpdatedUser");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Comment", b =>
                {
                    b.HasOne("BlogArray.Domain.Entities.AppUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BlogArray.Domain.Entities.Comment", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("BlogArray.Domain.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogArray.Domain.Entities.AppUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedUser");

                    b.Navigation("Parent");

                    b.Navigation("Post");

                    b.Navigation("UpdatedUser");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Post", b =>
                {
                    b.HasOne("BlogArray.Domain.Entities.AppUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BlogArray.Domain.Entities.AppUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedUser");

                    b.Navigation("UpdatedUser");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.PostRevision", b =>
                {
                    b.HasOne("BlogArray.Domain.Entities.AppUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BlogArray.Domain.Entities.Post", "Post")
                        .WithMany("PostRevisions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.PostTerm", b =>
                {
                    b.HasOne("BlogArray.Domain.Entities.Post", "Post")
                        .WithMany("Terms")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogArray.Domain.Entities.Term", "Term")
                        .WithMany("PostTerms")
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Term");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Statistic", b =>
                {
                    b.HasOne("BlogArray.Domain.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Storage", b =>
                {
                    b.HasOne("BlogArray.Domain.Entities.AppUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedUser");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostRevisions");

                    b.Navigation("Terms");
                });

            modelBuilder.Entity("BlogArray.Domain.Entities.Term", b =>
                {
                    b.Navigation("PostTerms");
                });
#pragma warning restore 612, 618
        }
    }
}
