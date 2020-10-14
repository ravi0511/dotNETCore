using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPwebappEmpty.Migrations
{
    public partial class InitialMigrtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Department = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "FileName", "Name" },
                values: new object[] { 1, 1, null, "Ravi" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Department", "FileName", "Name" },
                values: new object[] { 2, 2, null, "Prakas" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
