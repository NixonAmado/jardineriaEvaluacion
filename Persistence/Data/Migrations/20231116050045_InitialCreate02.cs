using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "state_id",
                table: "country");

            migrationBuilder.DropForeignKey(
                name: "city_id",
                table: "state");

            migrationBuilder.DropIndex(
                name: "state_id_idx",
                table: "country");

            migrationBuilder.DropColumn(
                name: "state_id",
                table: "country");

            migrationBuilder.RenameColumn(
                name: "city_id",
                table: "state",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "city_id_idx",
                table: "state",
                newName: "IX_state_CountryId");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "city",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_city_StateId",
                table: "city",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_city_state_StateId",
                table: "city",
                column: "StateId",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "city_id",
                table: "state",
                column: "CountryId",
                principalTable: "country",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_city_state_StateId",
                table: "city");

            migrationBuilder.DropForeignKey(
                name: "city_id",
                table: "state");

            migrationBuilder.DropIndex(
                name: "IX_city_StateId",
                table: "city");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "city");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "state",
                newName: "city_id");

            migrationBuilder.RenameIndex(
                name: "IX_state_CountryId",
                table: "state",
                newName: "city_id_idx");

            migrationBuilder.AddColumn<int>(
                name: "state_id",
                table: "country",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "state_id_idx",
                table: "country",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                name: "state_id",
                table: "country",
                column: "state_id",
                principalTable: "state",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "city_id",
                table: "state",
                column: "city_id",
                principalTable: "city",
                principalColumn: "id");
        }
    }
}
