using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatementApplication.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Statement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statement_ApplicationId",
                table: "Statement",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statement_Applications_ApplicationId",
                table: "Statement",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statement_Applications_ApplicationId",
                table: "Statement");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Statement_ApplicationId",
                table: "Statement");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Statement");
        }
    }
}
