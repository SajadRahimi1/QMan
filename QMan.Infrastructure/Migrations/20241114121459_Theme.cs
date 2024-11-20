using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Theme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedColor",
                table: "Businesses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedTheme",
                table: "Businesses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PersianTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThemeColor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PersianTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThemeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeColor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThemeColor_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThemeColor_ThemeId",
                table: "ThemeColor",
                column: "ThemeId");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT QMan.dbo.Theme ON
GO

INSERT INTO QMan.dbo.Theme (Id, EnglishTitle, PersianTitle, CreatedDateTime, UpdateDateTime) VALUES (1, N'fantasy', N'', N'2024-11-14 12:35:16.9566667', N'2024-11-14 12:35:16.9566667');
INSERT INTO QMan.dbo.Theme (Id, EnglishTitle, PersianTitle, CreatedDateTime, UpdateDateTime) VALUES (2, N'local', N'', N'2024-11-14 12:35:17.1200000', N'2024-11-14 12:35:17.1200000');
INSERT INTO QMan.dbo.Theme (Id, EnglishTitle, PersianTitle, CreatedDateTime, UpdateDateTime) VALUES (3, N'minimal', N'', N'2024-11-14 12:35:17.1633333', N'2024-11-14 12:35:17.1633333');
INSERT INTO QMan.dbo.Theme (Id, EnglishTitle, PersianTitle, CreatedDateTime, UpdateDateTime) VALUES (4, N'modern', N'', N'2024-11-14 12:35:17.2066667', N'2024-11-14 12:35:17.2066667');
INSERT INTO QMan.dbo.Theme (Id, EnglishTitle, PersianTitle, CreatedDateTime, UpdateDateTime) VALUES (5, N'sonnati', N'', N'2024-11-14 12:35:17.2733333', N'2024-11-14 12:35:17.2733333');
SET IDENTITY_INSERT QMan.dbo.Theme OFF
GO

SET IDENTITY_INSERT QMan.dbo.ThemeColor ON


INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (1, N'fantasyBlue', N'', 1, N'2024-11-14 12:38:41.2766667', N'2024-11-14 12:38:41.2766667');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (2, N'fantasyGold', N'', 1, N'2024-11-14 12:38:41.3300000', N'2024-11-14 12:38:41.3300000');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (3, N'fantasyMint', N'', 1, N'2024-11-14 12:38:41.3600000', N'2024-11-14 12:38:41.3600000');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (4, N'fantasyPink', N'', 1, N'2024-11-14 12:38:41.4166667', N'2024-11-14 12:38:41.4166667');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (5, N'fantasyPurple', N'', 1, N'2024-11-14 12:38:41.4500000', N'2024-11-14 12:38:41.4500000');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (6, N'localBlue', N'', 2, N'2024-11-14 12:39:57.8000000', N'2024-11-14 12:39:57.8000000');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (7, N'localMain', N'', 2, N'2024-11-14 12:39:57.8500000', N'2024-11-14 12:39:57.8500000');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (8, N'localMint', N'', 2, N'2024-11-14 12:39:57.8966667', N'2024-11-14 12:39:57.8966667');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (9, N'localOrange', N'', 2, N'2024-11-14 12:39:57.9400000', N'2024-11-14 12:39:57.9400000');
INSERT INTO QMan.dbo.ThemeColor (Id, EnglishTitle, PersianTitle, ThemeId, CreatedDateTime, UpdateDateTime) VALUES (10, N'localYellow', N'', 2, N'2024-11-14 12:39:57.9833333', N'2024-11-14 12:39:57.9833333');
SET IDENTITY_INSERT QMan.dbo.ThemeColor OFF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThemeColor");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropColumn(
                name: "SelectedColor",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "SelectedTheme",
                table: "Businesses");
        }
    }
}
