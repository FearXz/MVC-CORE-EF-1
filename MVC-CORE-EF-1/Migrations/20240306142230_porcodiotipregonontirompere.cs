using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_CORE_EF_1.Migrations
{
    /// <inheritdoc />
    public partial class porcodiotipregonontirompere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingDetails",
                columns: table => new
                {
                    IdShippingDetail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSpedizione = table.Column<int>(type: "int", nullable: false),
                    StatoSpedizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuogoCorrente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoteSpedizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAggiornamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingDetails", x => x.IdShippingDetail);
                    table.ForeignKey(
                        name: "FK_ShippingDetails_Shippings_IdSpedizione",
                        column: x => x.IdSpedizione,
                        principalTable: "Shippings",
                        principalColumn: "IdSpedizione",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_IdUser",
                table: "Shippings",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_IdSpedizione",
                table: "ShippingDetails",
                column: "IdSpedizione");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Users_IdUser",
                table: "Shippings",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Users_IdUser",
                table: "Shippings");

            migrationBuilder.DropTable(
                name: "ShippingDetails");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_IdUser",
                table: "Shippings");
        }
    }
}
