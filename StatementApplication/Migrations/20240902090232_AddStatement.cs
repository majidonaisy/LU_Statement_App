using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatementApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddStatement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleNameEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameAra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleNameAra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameAra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    POB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phonenumber = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statement_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statement_StudentId",
                table: "Statement",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statement");
        }
    }
}
