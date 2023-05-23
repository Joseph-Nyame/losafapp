using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOSAFAPI.Migrations.ReportItemsDb
{
    /// <inheritdoc />
    public partial class updatereportitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "ReportItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "ReportItems");
        }
    }
}
