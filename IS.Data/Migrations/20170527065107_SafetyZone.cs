using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations
{
    public partial class SafetyZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SafetyZones",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyZones_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SafetyZones_AccountId",
                table: "SafetyZones",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SafetyZones");
        }
    }
}
