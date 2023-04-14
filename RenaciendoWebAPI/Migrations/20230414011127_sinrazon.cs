using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenaciendoWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class sinrazon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Razon",
                table: "Renaceres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Razon",
                table: "Renaceres",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
