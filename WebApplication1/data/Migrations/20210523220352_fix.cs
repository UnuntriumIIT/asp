using Microsoft.EntityFrameworkCore.Migrations;

namespace data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRequired",
                table: "TodoItems",
                newName: "isCompleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCompleted",
                table: "TodoItems",
                newName: "isRequired");
        }
    }
}
