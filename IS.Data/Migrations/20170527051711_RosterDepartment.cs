using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations
{
    public partial class RosterDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RosterMembers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RosterMembers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RosterMembers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AccountId",
                table: "Departments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterMembers_AccountId",
                table: "RosterMembers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterMembers_DepartmentId",
                table: "RosterMembers",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RosterMembers");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
