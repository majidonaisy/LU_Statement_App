using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatementApplication.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationStudentConn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StudentId",
                table: "Applications",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Students_StudentId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_StudentId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Applications");
        }
    }
}
