using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelNest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstMigrationNumber2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookEntities_FolderEntities_FolderID",
                table: "BookEntities");

            migrationBuilder.AlterColumn<int>(
                name: "FolderID",
                table: "BookEntities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookEntities_FolderEntities_FolderID",
                table: "BookEntities",
                column: "FolderID",
                principalTable: "FolderEntities",
                principalColumn: "FolderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookEntities_FolderEntities_FolderID",
                table: "BookEntities");

            migrationBuilder.AlterColumn<int>(
                name: "FolderID",
                table: "BookEntities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookEntities_FolderEntities_FolderID",
                table: "BookEntities",
                column: "FolderID",
                principalTable: "FolderEntities",
                principalColumn: "FolderID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
