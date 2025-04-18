using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupCountBE.Infrastacture.Migrations
{
    /// <inheritdoc />
    public partial class CheckDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatdAt",
                table: "Users",
                newName: "UpdatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdatdAt");
        }
    }
}
