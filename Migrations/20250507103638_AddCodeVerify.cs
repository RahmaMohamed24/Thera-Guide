using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheraGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeVerify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "AspNetUsers");
        }
    }
}
