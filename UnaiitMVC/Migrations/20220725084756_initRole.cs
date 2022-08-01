using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnaiitMVC.Migrations
{
    public partial class initRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                  "Roles",
                  columns: new[] {
                       "Id",
                       "ConcurrencyStamp",
                       "Name",
                       "NormalizedName"
                  },
                  values: new object[] {
                       "0c00ae63-00a7-463c-98b7-9464e1a5c027",
                       Guid.NewGuid().ToString(),
                       "Admin",
                       "ADMIN"
                  });

            migrationBuilder.InsertData(
                   "Roles",
                   columns: new[] {
                       "Id",
                       "ConcurrencyStamp",
                       "Name",
                       "NormalizedName"
                   },
                   values: new object[] {
                       Guid.NewGuid().ToString(),
                       Guid.NewGuid().ToString(),
                       "Student",
                       "STUDENT"
                   });

            migrationBuilder.InsertData(
                   "Roles",
                   columns: new[] {
                       "Id",
                       "ConcurrencyStamp",
                       "Name",
                       "NormalizedName"
                   },
                   values: new object[] {
                       Guid.NewGuid().ToString(),
                       Guid.NewGuid().ToString(),
                       "Teacher",
                       "TEACHER"
                   });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
