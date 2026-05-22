using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditorium",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Auditoriumnumber = table.Column<double>(type: "float", nullable: false),
                    Auditoriumcapacity = table.Column<double>(type: "float", nullable: false),
                    Auditoriumsubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schoolchildren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dateofbirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schoolchildren", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assessmentfortheoge = table.Column<double>(type: "float", nullable: false),
                    Academicyearassessment = table.Column<double>(type: "float", nullable: false),
                    Finalassessment = table.Column<double>(type: "float", nullable: false),
                    SchoolchildrenId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Schoolchildren_SchoolchildrenId",
                        column: x => x.SchoolchildrenId,
                        principalTable: "Schoolchildren",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SchoolchildrenId",
                table: "Subject",
                column: "SchoolchildrenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorium");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Schoolchildren");
        }
    }
}
