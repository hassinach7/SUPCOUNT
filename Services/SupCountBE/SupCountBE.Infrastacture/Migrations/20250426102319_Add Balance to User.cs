﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupCountBE.Infrastacture.Migrations
{
    /// <inheritdoc />
    public partial class AddBalancetoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Balance",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");
        }
    }
}
