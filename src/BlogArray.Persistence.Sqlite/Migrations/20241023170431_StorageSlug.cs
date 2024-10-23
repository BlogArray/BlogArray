using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogArray.Persistence.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class StorageSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Storages_Slug",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Storages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Storages",
                type: "TEXT",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_Slug",
                table: "Storages",
                column: "Slug",
                unique: true);
        }
    }
}
