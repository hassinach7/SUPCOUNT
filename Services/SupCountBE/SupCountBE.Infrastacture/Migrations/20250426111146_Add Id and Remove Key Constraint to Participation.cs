using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupCountBE.Infrastacture.Migrations
{
    /// <inheritdoc />
    public partial class AddIdandRemoveKeyConstrainttoParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Participations",
                table: "Participations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Participations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participations",
                table: "Participations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_UserId",
                table: "Participations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Participations",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_UserId",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Participations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participations",
                table: "Participations",
                columns: new[] { "UserId", "ExpenseId" });
        }
    }
}
