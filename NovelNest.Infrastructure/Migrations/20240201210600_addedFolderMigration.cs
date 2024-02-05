using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelNest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedFolderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolderEntityFolderID",
                table: "BookEntities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FolderEntities",
                columns: table => new
                {
                    FolderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FolderDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderEntities", x => x.FolderID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookEntities_FolderEntityFolderID",
                table: "BookEntities",
                column: "FolderEntityFolderID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookEntities_FolderEntities_FolderEntityFolderID",
                table: "BookEntities",
                column: "FolderEntityFolderID",
                principalTable: "FolderEntities",
                principalColumn: "FolderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookEntities_FolderEntities_FolderEntityFolderID",
                table: "BookEntities");

            migrationBuilder.DropTable(
                name: "FolderEntities");

            migrationBuilder.DropIndex(
                name: "IX_BookEntities_FolderEntityFolderID",
                table: "BookEntities");

            migrationBuilder.DropColumn(
                name: "FolderEntityFolderID",
                table: "BookEntities");
        }
    }
}
