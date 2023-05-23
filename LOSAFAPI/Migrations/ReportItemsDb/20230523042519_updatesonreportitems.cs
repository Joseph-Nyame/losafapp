using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOSAFAPI.Migrations.ReportItemsDb
{
    /// <inheritdoc />
    public partial class updatesonreportitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ReportItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "founderName",
                table: "ReportItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ReportItems");

            migrationBuilder.DropColumn(
                name: "founderName",
                table: "ReportItems");
        }
    }
}
