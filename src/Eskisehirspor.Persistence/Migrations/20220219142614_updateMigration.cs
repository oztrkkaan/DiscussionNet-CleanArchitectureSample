using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact.Persistence.Migrations
{
    public partial class updateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Contacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Contacts",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Contacts",
                newName: "Name");
        }
    }
}
