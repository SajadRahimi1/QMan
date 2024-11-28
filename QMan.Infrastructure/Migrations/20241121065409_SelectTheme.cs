using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SelectTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedColor",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "SelectedTheme",
                table: "Businesses");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedThemeColorId",
                table: "Businesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "BaseProducts",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_SelectedThemeColorId",
                table: "Businesses",
                column: "SelectedThemeColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_ThemeColors_SelectedThemeColorId",
                table: "Businesses",
                column: "SelectedThemeColorId",
                principalTable: "ThemeColors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_ThemeColors_SelectedThemeColorId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_SelectedThemeColorId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SelectedThemeColorId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "BaseProducts");

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
        }
    }
}
