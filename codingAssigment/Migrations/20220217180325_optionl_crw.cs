using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace codingAssigment.Migrations
{
    public partial class optionl_crw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Crews_CrewId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "CrewId",
                table: "Employees",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Crews_CrewId",
                table: "Employees",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Crews_CrewId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "CrewId",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Crews_CrewId",
                table: "Employees",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
