using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOSAFAPI.Migrations.AddRegisterItemsDb
{
    /// <inheritdoc />
    public partial class updateadditems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "founderContact",
                table: "ItemsRegisteration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "founderContact",
                table: "ItemsRegisteration");
        }
    }
}
