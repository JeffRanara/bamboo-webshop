using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeraldine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommonNameToBambooSpecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommonName",
                table: "BambooSpecies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommonName",
                table: "BambooSpecies");
        }
    }
}
