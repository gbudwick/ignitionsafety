using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations
{
    public partial class Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountLocked = table.Column<bool>(nullable: false),
                    AccountOwner = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    DateCanceled = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    PrimaryContactUserId = table.Column<string>(nullable: true),
                    ReadOnly = table.Column<bool>(nullable: false),
                    SecondaryContactUserId = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    TrialPeriodExpiration = table.Column<DateTime>(nullable: false),
                    TrialStartDate = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
