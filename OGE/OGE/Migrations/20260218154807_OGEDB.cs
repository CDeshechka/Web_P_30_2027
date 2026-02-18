using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGE.Migrations
{
    /// <inheritdoc />
    public partial class OGEDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schoolchildrens",
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
                    table.PrimaryKey("PK_Schoolchildrens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subjectname1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assessmentfortheoge1 = table.Column<double>(type: "float", nullable: false),
                    Academicyearassessment1 = table.Column<double>(type: "float", nullable: false),
                    Finalassessment1 = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subjectname2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assessmentfortheoge2 = table.Column<double>(type: "float", nullable: false),
                    Academicyearassessment2 = table.Column<double>(type: "float", nullable: false),
                    Finalassessment2 = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject2", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schoolchildrens");

            migrationBuilder.DropTable(
                name: "Subject1");

            migrationBuilder.DropTable(
                name: "Subject2");
        }
    }
}
