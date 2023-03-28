using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutchTreat.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 28, 17, 49, 25, 849, DateTimeKind.Utc).AddTicks(3437));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 28, 17, 34, 47, 418, DateTimeKind.Utc).AddTicks(4420));
        }
    }
}
