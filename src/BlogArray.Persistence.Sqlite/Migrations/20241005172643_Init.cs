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
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
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
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Bio = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    SecurityCodeHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityCodeIssuedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SecurityCodeIssueCount = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_Posts_AppUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_AppUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AppUsers",
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
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
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
                        name: "FK_Storages_AppUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AppUsers",
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
                    EditExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
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
                        name: "FK_Comments_AppUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_AppUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AppUsers",
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
                        name: "FK_PostRevisions_AppUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AppUsers",
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
                    { 1, true, "SiteInfo", 0, "{\"Title\":\"BlogArray\",\"Tagline\":\"Welcome to BlogArray! \\uD83C\\uDF1F\",\"Description\":\"Your open-source Headless CMS platform built with ASP.NET Core and Angular, designed to make creating personal blogs and websites effortless and enjoyable.\",\"IconUrl\":\"/content/images/logo.svg\",\"LogoUrl\":\"/content/images/logo.svg\",\"SiteAddress\":null,\"AdminEmail\":null,\"AllowUserRegistration\":false,\"DefaultUserRole\":4}" },
                    { 2, true, "SMTP", 0, "{\"Username\":\"localhost\",\"Password\":\"password\",\"Host\":\"localhost\",\"Port\":587,\"UseSSL\":false}" },
                    { 3, true, "Content", 0, "{\"HomePageContentType\":\"posts\",\"StaticHomePageUrl\":\"home\",\"ItemsPerPage\":10,\"SearchEngineVisibility\":true,\"DefaultCategoryId\":1,\"DefaultCoverImageUrl\":\"/content/images/page-image.webp\",\"EnableCommentsByDefault\":true,\"MaxFeaturedPosts\":5,\"UseInfiniteScroll\":false}" },
                    { 4, true, "Media", 0, "{\"SmallSize\":{\"MaxWidth\":150,\"MaxHeight\":150},\"MediumSize\":{\"MaxWidth\":500,\"MaxHeight\":500},\"LargeSize\":{\"MaxWidth\":1024,\"MaxHeight\":1024},\"OptimizeImages\":true,\"OptimizedQuality\":75,\"OrganizeUploads\":true}" },
                    { 5, true, "Comments", 0, "{\"RequireLogin\":false,\"AllowAnonymous\":true,\"RequireManualApproval\":false,\"DefaultAvatarUrl\":\"\",\"InitialCommentsPerPost\":10,\"CommentEditWindowMinutes\":15,\"MaxThreadDepth\":3}" },
                    { 6, true, "Menu:TopNav", 1, "[{\"Page\":\"Home\",\"Link\":\"/\",\"SubLinks\":[]},{\"Page\":\"Sri Ramalaya temple\",\"Link\":\"/page/ramalayam-temple\",\"SubLinks\":[]},{\"Page\":\"Contribute\",\"Link\":\"/page/contribute\",\"SubLinks\":[]},{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"Blog\",\"Link\":\"/blog/list\",\"SubLinks\":[]},{\"Page\":\"About\",\"Link\":\"/page/about\",\"SubLinks\":[]},{\"Page\":\"Contact\",\"Link\":\"/contact\",\"SubLinks\":[]}]" },
                    { 7, true, "Menu:FooterLinks", 1, "[{\"Page\":\"Home\",\"Link\":\"/\",\"SubLinks\":[]},{\"Page\":\"Sri Ramalaya temple\",\"Link\":\"/page/ramalayam-temple\",\"SubLinks\":[]},{\"Page\":\"Contribute\",\"Link\":\"/page/contribute\",\"SubLinks\":[]},{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"About\",\"Link\":\"/page/about\",\"SubLinks\":[]},{\"Page\":\"Contact\",\"Link\":\"/contact\",\"SubLinks\":[]},{\"Page\":\"Privacy Policy\",\"Link\":\"/page/privacy-policy\",\"SubLinks\":[]}]" },
                    { 8, true, "Menu:QuickLinks", 1, "[{\"Page\":\"Donate\",\"Link\":\"/page/donate\",\"SubLinks\":[]},{\"Page\":\"Blog\",\"Link\":\"/blog/list\",\"SubLinks\":[]}]" },
                    { 9, true, "Menus", 0, "{\"HeaderMenu\":null,\"HeaderMenu2\":null,\"HeaderMenu3\":null,\"SocialMenu\":null,\"SocialMenu2\":null,\"FooterMenu\":null,\"FooterMenu2\":null,\"FooterMenu3\":null,\"FooterMenu4\":null}" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "Admin", "ADMIN" },
                    { 2, "Editor", "EDITOR" },
                    { 3, "Author", "AUTHOR" },
                    { 4, "Subscriber", "SUBSCRIBER" }
                });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "Description", "Name", "Slug", "TermType" },
                values: new object[] { 1, "", "Uncategorized", "uncategorized", 0 });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "CreatedOn", "CreatedUserId", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "PasswordHash", "RoleId", "SecurityCodeHash", "SecurityCodeIssueCount", "SecurityCodeIssuedAt", "TwoFactorEnabled", "UpdatedOn", "UpdatedUserId", "Username" },
                values: new object[,]
                {
                    { 1, 0, null, new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893), null, "Admin", "admin@vtp.com", true, false, null, "$2a$11$xO.YQRxXr4KYzralfcHauuSmpiPpJy.YRQpJTt4QTZJsMauYIi3Ca", 1, null, 0, null, false, null, null, "admin" },
                    { 2, 0, null, new DateTime(2022, 7, 8, 16, 37, 32, 163, DateTimeKind.Utc).AddTicks(7893), null, null, "anonymous@vtp.com", false, true, null, null, 4, null, 0, null, false, null, null, "anonymous" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOptions_Key_OptionType",
                table: "AppOptions",
                columns: new[] { "Key", "OptionType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_NormalizedName",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CreatedUserId",
                table: "AppUsers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Email",
                table: "AppUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RoleId",
                table: "AppUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UpdatedUserId",
                table: "AppUsers",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Username",
                table: "AppUsers",
                column: "Username",
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
                name: "IX_Terms_Slug_TermType",
                table: "Terms",
                columns: new[] { "Slug", "TermType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOptions");

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
                name: "Terms");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppRoles");
        }
    }
}
