using Microsoft.EntityFrameworkCore.Migrations;

namespace APIMCA.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "rul_version",
                schema: "public",
                table: "rule_final",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rul_version",
                schema: "public",
                table: "rule",
                type: "varchar(20)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rul_version",
                schema: "public",
                table: "rule");

            migrationBuilder.AlterColumn<string>(
                name: "rul_version",
                schema: "public",
                table: "rule_final",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);
        }
    }
}
