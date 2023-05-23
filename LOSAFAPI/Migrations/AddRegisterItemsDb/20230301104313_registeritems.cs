using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOSAFAPI.Migrations.AddRegisterItemsDb
{
    /// <inheritdoc />
    public partial class registeritems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemsRegisteration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsRegisteration", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsRegisteration");
        }
    }
}
