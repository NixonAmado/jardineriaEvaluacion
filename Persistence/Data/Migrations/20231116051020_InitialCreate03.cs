using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "country_id",
                table: "address");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "address",
                newName: "City_id");

            migrationBuilder.RenameIndex(
                name: "country_id_idx",
                table: "address",
                newName: "City_id_idx");

            migrationBuilder.AddForeignKey(
                name: "city_id",
                table: "address",
                column: "City_id",
                principalTable: "city",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "city_id",
                table: "address");

            migrationBuilder.RenameColumn(
                name: "City_id",
                table: "address",
                newName: "country_id");

            migrationBuilder.RenameIndex(
                name: "City_id_idx",
                table: "address",
                newName: "country_id_idx");

            migrationBuilder.AddForeignKey(
                name: "country_id",
                table: "address",
                column: "country_id",
                principalTable: "country",
                principalColumn: "id");
        }
    }
}
