using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Game.Data.Migrations
{
    public partial class characterColorPosChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "PlayerCharacter",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "PlayerCharacter",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "PlayerCharacter");

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Characters",
                nullable: true);
        }
    }
}
