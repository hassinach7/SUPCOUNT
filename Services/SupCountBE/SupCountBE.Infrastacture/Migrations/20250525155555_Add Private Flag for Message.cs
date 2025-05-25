using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupCountBE.Infrastacture.Migrations
{
    /// <inheritdoc />
    public partial class AddPrivateFlagforMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Messages");
        }
    }
}
