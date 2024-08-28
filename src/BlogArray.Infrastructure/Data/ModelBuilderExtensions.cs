using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BlogArray.Infrastructure.Data;

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

        _ = builder.Entity<IdentityRole<int>>().HasData(
          new IdentityRole<int>
          {
              Id = 1,
              Name = "Admin",
              NormalizedName = "Admin".Normalize().ToUpper(),
              ConcurrencyStamp = "4877cca9-5a1c-472b-a130-98fdfb5a9b42"
          },
          new IdentityRole<int>
          {
              Id = 2,
              Name = "Editor",
              NormalizedName = "Editor".Normalize().ToUpper(),
              ConcurrencyStamp = "4877cca9-5a1c-472b-a130-98fdfb5a9b42"
          },
          new IdentityRole<int>
          {
              Id = 3,
              Name = "Author",
              NormalizedName = "Author".Normalize().ToUpper(),
              ConcurrencyStamp = "4877cca9-5a1c-472b-a130-98fdfb5a9b42"
          }
        );

        _ = builder.Entity<AppUser>().HasData(
          new AppUser
          {
              Id = 1,
              CreatedOn = new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893),
              DisplayName = "Admin",
              Email = "admin@vtp.com",
              NormalizedEmail = "admin@vtp.com".Normalize().ToUpper(),
              EmailConfirmed = true,
              UserName = "admin",
              NormalizedUserName = "admin".Normalize().ToUpper(),
              AccessFailedCount = 0,
              ConcurrencyStamp = "8e05ab5d-3251-4ad9-8128-ceba2f331d1a",
              SecurityStamp = "FIS7U5Q4NMTNFYCFY6APLEOV36B2SFDJ",
              PasswordHash = "AQAAAAIAAYagAAAAELFIWWskHI9+r0MoDtV9uOjsEAFbJk23ACkLY8bP2acdySuMNeOWFrkp1tKM/7zyFA=="
          },
          new AppUser
          {
              Id = 2,
              CreatedOn = new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893),
              Email = "anonymous@vtp.com",
              NormalizedEmail = "anonymous@vtp.com".Normalize().ToUpper(),
              EmailConfirmed = false,
              UserName = "anonymous",
              NormalizedUserName = "anonymous".Normalize().ToUpper(),
              AccessFailedCount = 0,
              LockoutEnabled = true,
              SecurityStamp = "FIS7U5Q4NMTNFYCFY6APLEOV36Q2AWER",
              ConcurrencyStamp = "4877cca9-5a1c-542b-a130-98fdfb5a1f31"
          }
        );

        _ = builder.Entity<IdentityUserRole<int>>().HasData(
          new IdentityUserRole<int>
          {
              RoleId = 1,
              UserId = 1
          }
        );

        _ = builder.Entity<AppOption>().HasData(
          new AppOption
          {
              Id = 1,
              Key = "SiteInfo",
              Value = JsonSerializer.Serialize(new SiteInfoVM
              {
                  Title = "Bloggery",
                  Tagline = "A robust blogging platform.",
                  Description = "It is a robust blogging platform that offers a wide range of features.",
                  Logo = "/content/images/logo.svg",
                  Icon = "/content/images/logo.svg"
              }),
              OptionType = OptionType.Options,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 2,
              Key = "SMTP",
              Value = JsonSerializer.Serialize(new SMTPOptionsVM
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
              Key = "PageOptions",
              Value = JsonSerializer.Serialize(new PageOptionsVM
              {
                  HomePage = "posts",
                  StaticHomePage = "home",
                  DefaultCover = "/content/images/page-image.webp",
                  ItemsPerPage = 10,
                  SearchEngineVisibility = true,
                  DefaultCategory = 1
              }),
              OptionType = OptionType.Options,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 4,
              Key = "Menu:TopNav",
              Value = JsonSerializer.Serialize(TopMenuLinks),
              OptionType = OptionType.Menu,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 5,
              Key = "Menu:FooterLinks",
              Value = JsonSerializer.Serialize(FooterLinks),
              OptionType = OptionType.Menu,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 6,
              Key = "Menu:QuickLinks",
              Value = JsonSerializer.Serialize(QuickLinks),
              OptionType = OptionType.Menu,
              AutoLoad = true
          },
          new AppOption
          {
              Id = 7,
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