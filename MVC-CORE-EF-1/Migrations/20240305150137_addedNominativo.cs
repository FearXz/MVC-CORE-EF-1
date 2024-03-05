using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_CORE_EF_1.Migrations
{
    /// <inheritdoc />
    public partial class addedNominativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nominativo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nominativo",
                table: "Users");
        }
    }
}
