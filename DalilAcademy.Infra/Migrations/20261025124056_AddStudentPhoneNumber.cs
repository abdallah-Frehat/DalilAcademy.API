using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalilAcademy.Infra.Migrations
{
    public partial class AddStudentPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Students");
        }
    }
}
