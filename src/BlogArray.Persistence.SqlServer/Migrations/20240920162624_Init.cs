using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogArray.Persistence.SqlServer.Migrations
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionType = table.Column<int>(type: "int", nullable: false),
                    AutoLoad = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TermType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    SecurityCodeHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityCodeIssuedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityCodeIssueCount = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Views = table.Column<int>(type: "int", nullable: false),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsWidePage = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ShowContactPage = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ShowHeading = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowAuthor = table.Column<bool>(type: "bit", nullable: false),
                    ShowSharingIcon = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AllowComments = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowCover = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CommentsCount = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssetType = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    AuthorEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AuthorSite = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    AuthorIP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RawContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParsedContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParsedContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLatest = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    EditorType = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true)
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
                    TermId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true)
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
