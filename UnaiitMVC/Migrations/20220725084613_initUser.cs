using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnaiitMVC.Migrations
{
    public partial class initUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 0; i < 100; i++)
            {
                migrationBuilder.InsertData(
                    "Users",
                    columns: new[] {
                        "Id",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                        "HomeAddress",
                        "DeletedAt",
                        "type",
                        "BirthDate",
                        "Name",
                        "NormalizedUserName",
                        "NormalizedEmail"
                    },
                    values: new object[] {
                        Guid.NewGuid().ToString(),
                        $"test{i}@gmail.com",
                        $"test{i}@gmail.com",
                        Guid.NewGuid().ToString(),
                        true,
                        false,
                        false,
                        true,
                        0,
                        "homeaddress",
                        "",
                        0,
                        "01/01/2020",
                        $"name{i}",
                        $"TEST{i}@GMAIL.COM",
                        $"TEST{i}@GMAIL.COM"
                    });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
