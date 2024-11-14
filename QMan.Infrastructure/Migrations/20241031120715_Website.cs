using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Website : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Users_UserId",
                table: "TicketMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TicketMessages_UserId",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Businesses");

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "TicketMessages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Businesses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Businesses",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "ManagerName",
                table: "Businesses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Businesses",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseProducts_SubCategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SubcategoryId = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseProductBusiness",
                columns: table => new
                {
                    BaseProductsId = table.Column<int>(type: "int", nullable: false),
                    BusinessesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseProductBusiness", x => new { x.BaseProductsId, x.BusinessesId });
                    table.ForeignKey(
                        name: "FK_BaseProductBusiness_BaseProducts_BaseProductsId",
                        column: x => x.BaseProductsId,
                        principalTable: "BaseProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseProductBusiness_Businesses_BusinessesId",
                        column: x => x.BusinessesId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_BusinessId",
                table: "TicketMessages",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProductBusiness_BusinessesId",
                table: "BaseProductBusiness",
                column: "BusinessesId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProducts_SubcategoryId",
                table: "BaseProducts",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BusinessId",
                table: "Products",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Businesses_BusinessId",
                table: "TicketMessages",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Businesses_BusinessId",
                table: "TicketMessages");

            migrationBuilder.DropTable(
                name: "BaseProductBusiness");

            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "BaseProducts");

            migrationBuilder.DropIndex(
                name: "IX_TicketMessages_BusinessId",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Businesses");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Businesses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Businesses",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManagerName",
                table: "Businesses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Businesses",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Telegram = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_UserId",
                table: "TicketMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Users_UserId",
                table: "TicketMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
