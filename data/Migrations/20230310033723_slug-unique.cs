using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class slugunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_State_Slug",
                table: "State");

            migrationBuilder.DropIndex(
                name: "IX_Language_LanguageCode",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Country_Slug",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_City_Slug",
                table: "City");

            migrationBuilder.CreateIndex(
                name: "IX_State_Slug",
                table: "State",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_LanguageCode",
                table: "Language",
                column: "LanguageCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Slug",
                table: "Country",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Slug",
                table: "City",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_State_Slug",
                table: "State");

            migrationBuilder.DropIndex(
                name: "IX_Language_LanguageCode",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Country_Slug",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_City_Slug",
                table: "City");

            migrationBuilder.CreateIndex(
                name: "IX_State_Slug",
                table: "State",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_Language_LanguageCode",
                table: "Language",
                column: "LanguageCode");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Slug",
                table: "Country",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_City_Slug",
                table: "City",
                column: "Slug");
        }
    }
}
