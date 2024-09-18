using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogArray.Persistence.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    OptionType = table.Column<int>(type: "INTEGER", nullable: false),
                    AutoLoad = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Bio = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                    Slug = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    TermType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Cover = table.Column<string>(type: "TEXT", maxLength: 160, nullable: true),
                    Views = table.Column<int>(type: "INTEGER", nullable: false),
                    PostType = table.Column<int>(type: "INTEGER", nullable: false),
                    PostStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsWidePage = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    ShowContactPage = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    ShowHeading = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    ShowAuthor = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowSharingIcon = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    AllowComments = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    ShowCover = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    CommentsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AssetType = table.Column<int>(type: "INTEGER", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    Length = table.Column<long>(type: "INTEGER", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Author = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    AuthorEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AuthorSite = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    AuthorIP = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    RawContent = table.Column<string>(type: "TEXT", nullable: false),
                    ParsedContent = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostRevisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RawContent = table.Column<string>(type: "TEXT", nullable: false),
                    ParsedContent = table.Column<string>(type: "TEXT", nullable: false),
                    IsLatest = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    EditorType = table.Column<int>(type: "INTEGER", nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostRevisions_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostRevisions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTerms",
                columns: table => new
                {
                    TermId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTerms", x => new { x.PostId, x.TermId });
                    table.ForeignKey(
                        name: "FK_PostTerms_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTerms_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    ViewedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistics_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AppOptions",
                columns: new[] { "Id", "AutoLoad", "Key", "OptionType", "Value" },
                values: new object[,]
                {
                    { 1, true, "SiteInfo", 0, "{\"Title\":\"Bloggery\",\"Tagline\":\"A robust blogging platform.\",\"Description\":\"It is a robust blogging platform that offers a wide range of features.\",\"Icon\":\"/content/images/logo.svg\",\"Logo\":\"/content/images/logo.svg\"}" },
                    { 2, true, "SMTP", 0, "{\"Username\":\"localhost\",\"Password\":\"password\",\"Host\":\"localhost\",\"Port\":587,\"UseSSL\":false}" },
                    { 3, true, "PageOptions", 0, "{\"HomePage\":\"posts\",\"StaticHomePage\":\"home\",\"DefaultCategory\":1,\"DefaultCover\":\"/content/images/page-image.webp\",\"ItemsPerPage\":10,\"SearchEngineVisibility\":true}" },
                    { 4, true, "Menu:TopNav", 1, "[{\"Page\":\"Home\",\"Link\":\"/\",\"SubLinks\":[]},{\"Page\":\"Sri Ramalaya temple\",\"Link\":\"/page/ramalayam-temple\",\"SubLinks\":[]},{\"Page\":\"Contribute\",\"Link\":\"/page/contribute\",\"SubLinks\":[]},{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"Blog\",\"Link\":\"/blog/list\",\"SubLinks\":[]},{\"Page\":\"About\",\"Link\":\"/page/about\",\"SubLinks\":[]},{\"Page\":\"Contact\",\"Link\":\"/contact\",\"SubLinks\":[]}]" },
                    { 5, true, "Menu:FooterLinks", 1, "[{\"Page\":\"Home\",\"Link\":\"/\",\"SubLinks\":[]},{\"Page\":\"Sri Ramalaya temple\",\"Link\":\"/page/ramalayam-temple\",\"SubLinks\":[]},{\"Page\":\"Contribute\",\"Link\":\"/page/contribute\",\"SubLinks\":[]},{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"About\",\"Link\":\"/page/about\",\"SubLinks\":[]},{\"Page\":\"Contact\",\"Link\":\"/contact\",\"SubLinks\":[]},{\"Page\":\"Privacy Policy\",\"Link\":\"/page/privacy-policy\",\"SubLinks\":[]}]" },
                    { 6, true, "Menu:QuickLinks", 1, "[{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"Blog\",\"Link\":\"/blog/list\",\"SubLinks\":[]}]" },
                    { 7, true, "Menus", 0, "{\"HeaderMenu\":null,\"HeaderMenu2\":null,\"HeaderMenu3\":null,\"SocialMenu\":null,\"SocialMenu2\":null,\"FooterMenu\":null,\"FooterMenu2\":null,\"FooterMenu3\":null,\"FooterMenu4\":null}" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "4877cca9-5a1c-472b-a130-98fdfb5a9b42", "Admin", "ADMIN" },
                    { 2, "5b20c5f7-175f-4b5a-a621-c36d42719f0c", "Editor", "EDITOR" },
                    { 3, "0aa90b5e-9dc3-496f-8b7d-13f235d2457f", "Author", "AUTHOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "CreatedOn", "CreatedUserId", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedOn", "UpdatedUserId", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "8e05ab5d-3251-4ad9-8128-ceba2f331d1a", new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893), null, "Admin", "admin@vtp.com", true, false, null, "ADMIN@VTP.COM", "ADMIN", "AQAAAAIAAYagAAAAELFIWWskHI9+r0MoDtV9uOjsEAFbJk23ACkLY8bP2acdySuMNeOWFrkp1tKM/7zyFA==", null, false, "FIS7U5Q4NMTNFYCFY6APLEOV36B2SFDJ", false, null, null, "admin" },
                    { 2, 0, null, "4877cca9-5a1c-542b-a130-98fdfb5a1f31", new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893), null, null, "anonymous@vtp.com", false, true, null, "ANONYMOUS@VTP.COM", "ANONYMOUS", null, null, false, "FIS7U5Q4NMTNFYCFY6APLEOV36Q2AWER", false, null, null, "anonymous" }
                });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "Description", "Name", "Slug", "TermType" },
                values: new object[] { 1, "", "Uncategorized", "uncategorized", 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AppOptions_Key_OptionType",
                table: "AppOptions",
                columns: new[] { "Key", "OptionType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreatedUserId",
                table: "AspNetUsers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UpdatedUserId",
                table: "AspNetUsers",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedUserId",
                table: "Comments",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UpdatedUserId",
                table: "Comments",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRevisions_CreatedUserId",
                table: "PostRevisions",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRevisions_PostId",
                table: "PostRevisions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedUserId",
                table: "Posts",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Slug",
                table: "Posts",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UpdatedUserId",
                table: "Posts",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTerms_PostId_TermId",
                table: "PostTerms",
                columns: new[] { "PostId", "TermId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTerms_TermId",
                table: "PostTerms",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_PostId",
                table: "Statistics",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_CreatedUserId",
                table: "Storages",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_Slug",
                table: "Storages",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terms_Slug",
                table: "Terms",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOptions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PostRevisions");

            migrationBuilder.DropTable(
                name: "PostTerms");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
