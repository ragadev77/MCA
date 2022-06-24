using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APIMCA.Migrations
{
    public partial class Recreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "rul_is_deleted",
                schema: "public",
                table: "rule_final",
                type: "boolean",
                nullable: true,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "rule",
                schema: "public",
                columns: table => new
                {
                    rul_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    rul_name = table.Column<string>(type: "varchar(20)", nullable: true),
                    rul_desc = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_condition = table.Column<string>(type: "varchar(100)", nullable: true),
                    rul_output = table.Column<string>(type: "varchar(20)", nullable: true),
                    rul_type = table.Column<int>(type: "integer", nullable: false),
                    rul_is_active = table.Column<bool>(type: "boolean", nullable: false),
                    rul_created_by = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_modified_by = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_is_deleted = table.Column<bool>(type: "boolean", nullable: true),
                    rul_is_used = table.Column<bool>(type: "boolean", nullable: false),
                    rul_created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    rul_modified = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_output_type = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_approved_status = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_approved_by = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_category = table.Column<string>(type: "varchar(50)", nullable: true),
                    rul_applied = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rule", x => x.rul_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rule",
                schema: "public");

            migrationBuilder.AlterColumn<bool>(
                name: "rul_is_deleted",
                schema: "public",
                table: "rule_final",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "false");
        }
    }
}
