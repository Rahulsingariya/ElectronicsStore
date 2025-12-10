using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicsStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(7935));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(7936));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8120));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8121));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 14, 7, 39, 790, DateTimeKind.Local).AddTicks(8125));
        }
    }
}
