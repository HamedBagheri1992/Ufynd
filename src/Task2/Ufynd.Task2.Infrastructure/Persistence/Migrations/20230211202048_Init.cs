using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ufynd.Task2.Infrastructure.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoProcessing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoProcessing", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoProcessing_SendTime_Email",
                table: "AutoProcessing",
                columns: new[] { "SendTime", "Email" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoProcessing");
        }
    }
}
