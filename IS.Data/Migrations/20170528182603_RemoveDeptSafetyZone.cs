using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations
{
    public partial class RemoveDeptSafetyZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentSafetyZones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentSafetyZones",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true),
                    SafetyZoneId = table.Column<string>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentSafetyZones", x => x.Id);
                });
        }
    }
}
