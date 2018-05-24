using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations
{
    public partial class AddSafetyZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SafetyZoneId",
                table: "Departments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SafetyZoneId",
                table: "Departments",
                column: "SafetyZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_SafetyZones_SafetyZoneId",
                table: "Departments",
                column: "SafetyZoneId",
                principalTable: "SafetyZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_SafetyZones_SafetyZoneId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SafetyZoneId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SafetyZoneId",
                table: "Departments");
        }
    }
}
