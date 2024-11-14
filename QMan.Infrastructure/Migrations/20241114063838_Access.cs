using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Access : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Access",
                table: "Admins");

            migrationBuilder.CreateTable(
                name: "Accesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessAdmin",
                columns: table => new
                {
                    AccessId = table.Column<int>(type: "int", nullable: false),
                    AdminsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessAdmin", x => new { x.AccessId, x.AdminsId });
                    table.ForeignKey(
                        name: "FK_AccessAdmin_Accesses_AccessId",
                        column: x => x.AccessId,
                        principalTable: "Accesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessAdmin_Admins_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessAdmin_AdminsId",
                table: "AccessAdmin",
                column: "AdminsId");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT QMan.dbo.Accesses ON
GO

INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (1, N'صفحه پیشخوان', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (2, N'صفحه کسب و کارها', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (3, N'صفحه نظرات', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (4, N'صفحه مدیران', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (5, N'صفحه تراکنش مالی', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (6, N'صفحه تیکت', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (7, N'مالی', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (8, N'مدیریت', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (9, N'بسته شده', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (10, N'تغییر وضعیت تیکت', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (11, N'پاسخ به تیکت', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (12, N'حذف تیکت', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (13, N'درانتظار بررسی', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (14, N'پشتیبانی', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (15, N'محصول', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (16, N'مالی', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (17, N'آخرین پرداختی ها', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (18, N'تعداد کسب و کار', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (19, N'مجموع مبلغ فروش', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (20, N'تغییر وضعیت ادمین', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (21, N'افزودن ادمین', null, null);
INSERT INTO QMan.dbo.Accesses (Id, Title, CreatedDateTime, UpdateDateTime) VALUES (22, N'ویرایش ادمین', null, null);
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessAdmin");

            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.AddColumn<string>(
                name: "Access",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
