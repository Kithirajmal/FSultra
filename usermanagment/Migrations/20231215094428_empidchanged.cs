using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace usermanagment.Migrations
{
    /// <inheritdoc />
    public partial class empidchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "empId",
                table: "Resourceallocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "empId",
                table: "Resourceallocations");
        }
    }
}
