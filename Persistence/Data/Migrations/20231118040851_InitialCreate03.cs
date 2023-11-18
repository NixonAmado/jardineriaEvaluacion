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
                name: "order_id",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "product_id",
                table: "order_detail");

            migrationBuilder.UpdateData(
                table: "order_detail",
                keyColumn: "product_id",
                keyValue: null,
                column: "product_id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "product_id",
                table: "order_detail",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                table: "order_detail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "order_detail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "order_detail",
                type: "varchar(15)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "order",
                type: "varchar(15)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_detail",
                table: "order_detail",
                columns: new[] { "order_id", "product_id" });

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_OrderId1",
                table: "order_detail",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_ProductId1",
                table: "order_detail",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_order_OrderId",
                table: "order",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_ProductId",
                table: "order",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_order_OrderId",
                table: "order",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_ProductId",
                table: "order",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_detail_order_OrderId1",
                table: "order_detail",
                column: "OrderId1",
                principalTable: "order",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_detail_product_ProductId1",
                table: "order_detail",
                column: "ProductId1",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "order_id",
                table: "order_detail",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "product_id",
                table: "order_detail",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_order_OrderId",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_product_ProductId",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_detail_order_OrderId1",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_order_detail_product_ProductId1",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "order_id",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "product_id",
                table: "order_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_detail",
                table: "order_detail");

            migrationBuilder.DropIndex(
                name: "IX_order_detail_OrderId1",
                table: "order_detail");

            migrationBuilder.DropIndex(
                name: "IX_order_detail_ProductId1",
                table: "order_detail");

            migrationBuilder.DropIndex(
                name: "IX_order_OrderId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_ProductId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "order_detail");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "order_detail");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "order");

            migrationBuilder.AlterColumn<string>(
                name: "product_id",
                table: "order_detail",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                table: "order_detail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "order_id",
                table: "order_detail",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "product_id",
                table: "order_detail",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id");
        }
    }
}
