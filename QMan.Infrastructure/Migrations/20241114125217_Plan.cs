using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeColor_Theme_ThemeId",
                table: "ThemeColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThemeColor",
                table: "ThemeColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Theme",
                table: "Theme");

            migrationBuilder.RenameTable(
                name: "ThemeColor",
                newName: "ThemeColors");

            migrationBuilder.RenameTable(
                name: "Theme",
                newName: "Themes");

            migrationBuilder.RenameIndex(
                name: "IX_ThemeColor_ThemeId",
                table: "ThemeColors",
                newName: "IX_ThemeColors_ThemeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThemeColors",
                table: "ThemeColors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Themes",
                table: "Themes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDay = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeColors_Themes_ThemeId",
                table: "ThemeColors",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT QMan.dbo.Plans ON
GO

INSERT INTO QMan.dbo.Plans (Id, Title, ExpirationDay, Price, CreatedDateTime, UpdateDateTime) VALUES (1, N'1 ماهه', 30, N'0', N'2024-11-14 16:23:25.0000000', null);
INSERT INTO QMan.dbo.Plans (Id, Title, ExpirationDay, Price, CreatedDateTime, UpdateDateTime) VALUES (2, N'3 ماهه', 90, N'1200000', N'2024-11-14 16:24:09.0000000', null);
INSERT INTO QMan.dbo.Plans (Id, Title, ExpirationDay, Price, CreatedDateTime, UpdateDateTime) VALUES (3, N'6 ماهه', 180, N'2400000', N'2024-11-14 16:24:36.0000000', null);
INSERT INTO QMan.dbo.Plans (Id, Title, ExpirationDay, Price, CreatedDateTime, UpdateDateTime) VALUES (4, N'12 ماهه', 360, N'4400000', N'2024-11-14 16:25:17.0000000', null);

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeColors_Themes_ThemeId",
                table: "ThemeColors");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Themes",
                table: "Themes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThemeColors",
                table: "ThemeColors");

            migrationBuilder.RenameTable(
                name: "Themes",
                newName: "Theme");

            migrationBuilder.RenameTable(
                name: "ThemeColors",
                newName: "ThemeColor");

            migrationBuilder.RenameIndex(
                name: "IX_ThemeColors_ThemeId",
                table: "ThemeColor",
                newName: "IX_ThemeColor_ThemeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Theme",
                table: "Theme",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThemeColor",
                table: "ThemeColor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeColor_Theme_ThemeId",
                table: "ThemeColor",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
