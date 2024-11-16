using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatementApplication.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveredBy",
                table: "Statement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoneBy",
                table: "Statement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredBy",
                table: "Statement");

            migrationBuilder.DropColumn(
                name: "DoneBy",
                table: "Statement");
        }
    }
}
