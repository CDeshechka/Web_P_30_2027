using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGE.Migrations
{
    /// <inheritdoc />
    public partial class IgnoreSchoolchildrenName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Schoolchildren_SchoolchildrenId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Schoolchildren");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Schoolchildren_SchoolchildrenId",
                table: "Subject",
                column: "SchoolchildrenId",
                principalTable: "Schoolchildren",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Schoolchildren_SchoolchildrenId",
                table: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Schoolchildren",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Schoolchildren_SchoolchildrenId",
                table: "Subject",
                column: "SchoolchildrenId",
                principalTable: "Schoolchildren",
                principalColumn: "Id");
        }
    }
}
