using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations
{
    public partial class MemberStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MembersStatuses",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    MemberId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersStatuses", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembersStatuses");
        }
    }
}
