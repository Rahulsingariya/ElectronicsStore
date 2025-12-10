using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicsStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(6934));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(6936));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(6938));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(6940));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(6941));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7110));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7112));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7114));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7116));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7118));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 28, 11, 43, 34, 738, DateTimeKind.Local).AddTicks(7120));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(798));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(804));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(806));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(809));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(812));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1574));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1580));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1594));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1597));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 22, 43, 42, 719, DateTimeKind.Local).AddTicks(1601));
        }
    }
}
