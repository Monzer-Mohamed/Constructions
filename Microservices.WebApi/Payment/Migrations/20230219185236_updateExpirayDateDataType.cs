using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Migrations
{
    public partial class updateExpirayDateDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Payment");

            migrationBuilder.AddColumn<string>(
                name: "CardExpiry",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardExpiry",
                table: "Payment");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Payment",
                type: "datetime2",
                nullable: true);
        }
    }
}
