using Microsoft.EntityFrameworkCore.Migrations;

namespace Construction.Migrations
{
    public partial class updateTableNameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationForm",
                table: "ApplicationForm");

            migrationBuilder.RenameTable(
                name: "ApplicationForm",
                newName: "Construction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Construction",
                table: "Construction",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Construction",
                table: "Construction");

            migrationBuilder.RenameTable(
                name: "Construction",
                newName: "ApplicationForm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationForm",
                table: "ApplicationForm",
                column: "Id");
        }
    }
}
