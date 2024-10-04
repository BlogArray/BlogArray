using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BlogArray.Persistence;

public static class ModelBuilderExtensions
{
    private static readonly List<MenuLink> TopMenuLinks = GetTopMenuLinks();

    private static List<MenuLink> GetTopMenuLinks()
    {
        return [
          new()
          {
              Page = "Home",
              Link = "/"
          },
            new()
            {
                Page = "Sri Ramalaya temple",
                Link = "/page/ramalayam-temple"
            },
            new()
            {
                Page = "Contribute",
                Link = "/page/contribute"
            },
            new()
            {
                Page = "Donate",
                Link = "/page/donate"
            },
            new()
            {
                Page = "Blog",
                Link = "/blog/list"
            },
            new()
            {
                Page = "About",
                Link = "/page/about"
            },
            new()
            {
                Page = "Contact",
                Link = "/contact"
            }
        ];
    }

    private static readonly List<MenuLink> FooterLinks = GetFooterLinks();

    private static List<MenuLink> GetFooterLinks()
    {
        return [
          new()
          {
              Page = "Home",
              Link = "/"
          },
            new()
            {
                Page = "Sri Ramalaya temple",
                Link = "/page/ramalayam-temple"
            },
            new()
            {
                Page = "Contribute",
                Link = "/page/contribute"
            },
            new()
            {
                Page = "Donate",
                Link = "/page/donate"
            },
            new()
            {
                Page = "About",
                Link = "/page/about"
            },
            new()
            {
                Page = "Contact",
                Link = "/contact"
            },
            new()
            {
                Page = "Privacy Policy",
                Link = "/page/privacy-policy"
            }
        ];
    }

    private static readonly List<MenuLink> QuickLinks = GetQuickLinks();

    private static List<MenuLink> GetQuickLinks()
    {
        return [
          new()
          {
              Page = "Donate",
              Link = "/page/donate"
          },
            new()
            {
                Page = "Blog",
                Link = "/blog/list"
            }
        ];
    }

    public static void Seed(this ModelBuilder builder)
    {
        builder.Entity<AppRole>().HasData(
            new AppRole
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new AppRole
            {
                Id = 2,
                Name = "Editor",
                NormalizedName = "EDITOR"
            },
            new AppRole
            {
                Id = 3,
                Name = "Author",
                NormalizedName = "AUTHOR"
            },
            new AppRole
            {
                Id = 4,
                Name = "Subscriber",
                NormalizedName = "SUBSCRIBER"
            });

        builder.Entity<AppUser>().HasData(
          new AppUser
          {
              Id = 1,
              CreatedOn = new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893),
              DisplayName = "Admin",
              Email = "admin@vtp.com",
              EmailConfirmed = true,
              Username = "admin",
              AccessFailedCount = 0,
              PasswordHash = "$2a$11$xO.YQRxXr4KYzralfcHauuSmpiPpJy.YRQpJTt4QTZJsMauYIi3Ca",
              RoleId = 1,
          },
          new AppUser
          {
              Id = 2,
              CreatedOn = new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893),
              Email = "anonymous@vtp.com",
              EmailConfirmed = false,
              Username = "anonymous",
              AccessFailedCount = 0,
              LockoutEnabled = true,
              RoleId = 4,
          });

        builder.Entity<AppOption>().HasData(
          new AppOption
          {
              Id = 1,
              Key = "SiteInfo",
              Value = JsonSerializer.Serialize(new SiteInfo
              {
                  Title = "BlogArray",
                  Tagline = "Welcome to BlogArray! 🌟",
                  Description = "Your open-source Headless CMS platform built with ASP.NET Core and Angular, designed to make creating personal blogs and websites effortless and enjoyable.",
                  LogoUrl = "/content/images/logo.svg",
                  IconUrl = "/content/images/logo.svg",
                  AllowUserRegistration = false,
                  DefaultUserRole = 4
              }),
              OptionType = OptionType.Options,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 2,
              Key = "SMTP",
              Value = JsonSerializer.Serialize(new EmailSettings
              {
                  Username = "localhost",
                  Password = "password",
                  Host = "localhost",
                  Port = 587,
                  UseSSL = false
              }),
              OptionType = OptionType.Options,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 3,
              Key = "Content",
              Value = JsonSerializer.Serialize(new ContentSettings
              {
                  DefaultCoverImageUrl = "/content/images/page-image.webp",
                  DefaultCategoryId = 1,
                  HomePageContentType = "posts",
                  StaticHomePageUrl = "home",
                  ItemsPerPage = 10,
                  SearchEngineVisibility = true,
                  EnableCommentsByDefault = true,
                  MaxFeaturedPosts = 5,
                  UseInfiniteScroll = false
              }),
              OptionType = OptionType.Options,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 4,
              Key = "Media",
              Value = JsonSerializer.Serialize(new MediaSettings())
          },
          new AppOption
          {
              Id = 5,
              Key = "Comments",
              Value = JsonSerializer.Serialize(new CommentSettings()),
              OptionType = OptionType.Options,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 6,
              Key = "Menu:TopNav",
              Value = JsonSerializer.Serialize(TopMenuLinks),
              OptionType = OptionType.Menu,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 7,
              Key = "Menu:FooterLinks",
              Value = JsonSerializer.Serialize(FooterLinks),
              OptionType = OptionType.Menu,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 8,
              Key = "Menu:QuickLinks",
              Value = JsonSerializer.Serialize(QuickLinks),
              OptionType = OptionType.Menu,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 9,
              Key = "Menus",
              Value = JsonSerializer.Serialize(new AppMenus()),
              OptionType = OptionType.Options,
              AutoLoad = true
          });

        builder.Entity<Term>().HasData(new Term
        {
            Id = 1,
            Name = "Uncategorized",
            Slug = "uncategorized",
            Description = ""
        });
    }
}