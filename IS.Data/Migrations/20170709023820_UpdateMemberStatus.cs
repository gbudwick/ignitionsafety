using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations
{
    public partial class UpdateMemberStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "MembersStatuses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "MembersStatuses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Locked",
                table: "MembersStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "MembersStatuses",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
