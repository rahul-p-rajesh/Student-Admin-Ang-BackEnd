using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    RollNo = table.Column<int>(name: "Roll No", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RollNo);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    userType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "UserLogins");
        }
    }
}
