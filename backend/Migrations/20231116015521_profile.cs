using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Framework",
                columns: table => new
                {
                    FrameworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Framework", x => x.FrameworkId);
                    table.ForeignKey(
                        name: "FK_Framework_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hobby",
                columns: table => new
                {
                    HobbyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobby", x => x.HobbyId);
                    table.ForeignKey(
                        name: "FK_Hobby_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "ProfileId", "Age", "City", "Country", "Email", "Facebook", "ImageUrl", "Instagram", "Lastname", "Name", "Summary" },
                values: new object[] { 1, 22, "Antofagasta", "Chile", "joaquin.pinto@alumnos.ucn.cl", "https://www.facebook.com/", "https://images.mubicdn.net/images/cast_member/2552/cache-207-1524922850/image-w856.jpg?size=800x", "https://www.instagram.com/", "Pinto", "Joaquin", "Un joven estudiante dedicado a su familia y amigos con sueños de crecer como persona y profesional." });

            migrationBuilder.InsertData(
                table: "Framework",
                columns: new[] { "FrameworkId", "Level", "Name", "ProfileId", "Quantity", "Year" },
                values: new object[,]
                {
                    { 1, "Moderado", "React", 1, 50, 2022 },
                    { 2, "Alto", "HTML", 1, 80, 2020 },
                    { 3, "Bajo", "CSS", 1, 20, 2022 }
                });

            migrationBuilder.InsertData(
                table: "Hobby",
                columns: new[] { "HobbyId", "Description", "Name", "ProfileId" },
                values: new object[,]
                {
                    { 1, "Jugar porque fomenta la creatividad, la sociabilidad y el desarrollo cognitivo.", "Jugar videojuegos", 1 },
                    { 2, "Cantar ya que puede aliviar el estrés, mejorar el estado de ánimo y fortalecer las habilidades comunicativas.", "Cantar", 1 },
                    { 3, "Bailar porque combina actividad física con expresión artística, promoviendo la salud y el bienestar emocional.", "Bailar", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Framework_ProfileId",
                table: "Framework",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobby_ProfileId",
                table: "Hobby",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Framework");

            migrationBuilder.DropTable(
                name: "Hobby");

            migrationBuilder.DropTable(
                name: "Profile");
        }
    }
}
