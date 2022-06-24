using Microsoft.EntityFrameworkCore.Migrations;

namespace APIMCA.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rul_id_ori",
                schema: "public",
                table: "rule",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rul_id_ori",
                schema: "public",
                table: "rule");
        }
    }
}
