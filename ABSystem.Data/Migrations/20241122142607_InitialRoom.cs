using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Rooms",
               columns: table => new
               {
                   Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Facility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Capacity = table.Column<int>(type: "int", nullable: false),
                   Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   IsDeleted = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Room", x => x.Id);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
