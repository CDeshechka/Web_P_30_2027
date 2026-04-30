using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGE.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolchildrenIdToSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolchildrenId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SchoolchildrenId",
                table: "Subject",
                column: "SchoolchildrenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Schoolchildren_SchoolchildrenId",
                table: "Subject",
                column: "SchoolchildrenId",
                principalTable: "Schoolchildren",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Schoolchildren_SchoolchildrenId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SchoolchildrenId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "SchoolchildrenId",
                table: "Subject");
        }
    }
}
