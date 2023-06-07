using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaDesk_Razor.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DeliveryTypes",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTypeId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_DeliveryTypeId",
                table: "Quotes",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_MaterialId",
                table: "Quotes",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_DeliveryTypes_DeliveryTypeId",
                table: "Quotes",
                column: "DeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Materials_MaterialId",
                table: "Quotes",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_DeliveryTypes_DeliveryTypeId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Materials_MaterialId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_DeliveryTypeId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_MaterialId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "DeliveryTypes",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryType",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
