using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_project.Migrations
{
    /// <inheritdoc />
    public partial class UploadImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Instructores");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Instructores");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Instructores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Instructores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Instructores",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Instructores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
