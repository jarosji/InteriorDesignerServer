using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteriorDesigner.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Files_FileId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_FileId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FileId1",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FileId",
                table: "Projects",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Files_FileId",
                table: "Projects",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Files_FileId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_FileId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FileId1",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FileId1",
                table: "Projects",
                column: "FileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Files_FileId1",
                table: "Projects",
                column: "FileId1",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
