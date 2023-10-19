using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tourney_Lad.WebSite.Migrations
{
    /// <inheritdoc />
    public partial class mymigrationv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Players");
        }
    }
}
