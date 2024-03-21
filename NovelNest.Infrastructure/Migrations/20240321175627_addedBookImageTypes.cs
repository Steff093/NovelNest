using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelNest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedBookImageTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBook",
                table: "BookEntities",
                type: "varbinary(max)",
                maxLength: 5242880,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMIMEType",
                table: "BookEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBook",
                table: "BookEntities");

            migrationBuilder.DropColumn(
                name: "ImageMIMEType",
                table: "BookEntities");
        }
    }
}
