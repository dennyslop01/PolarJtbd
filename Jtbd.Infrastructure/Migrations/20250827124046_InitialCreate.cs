using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jtbd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deparments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deparments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    IdInter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InterAge = table.Column<int>(type: "int", nullable: false),
                    InterGender = table.Column<int>(type: "int", nullable: false),
                    InterOccupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InterNickname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InterNSE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DateInter = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.IdInter);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmployeeRol = table.Column<int>(type: "int", nullable: false),
                    DeparmentsId = table.Column<int>(type: "int", nullable: true),
                    StatusEmployee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Deparments_DeparmentsId",
                        column: x => x.DeparmentsId,
                        principalTable: "Deparments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    IdProject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeparmentId = table.Column<int>(type: "int", nullable: true),
                    CategoriesId = table.Column<int>(type: "int", nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MaxPushes = table.Column<int>(type: "int", nullable: false),
                    MaxPulls = table.Column<int>(type: "int", nullable: false),
                    RutaImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusProject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.IdProject);
                    table.ForeignKey(
                        name: "FK_Projects_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Deparments_DeparmentId",
                        column: x => x.DeparmentId,
                        principalTable: "Deparments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Anxieties",
                columns: table => new
                {
                    IdAnxie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnxieName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectIdProject = table.Column<int>(type: "int", nullable: true),
                    CreatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusAnxie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anxieties", x => x.IdAnxie);
                    table.ForeignKey(
                        name: "FK_Anxieties_Projects_ProjectIdProject",
                        column: x => x.ProjectIdProject,
                        principalTable: "Projects",
                        principalColumn: "IdProject");
                });

            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    IdHabit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HabitName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectIdProject = table.Column<int>(type: "int", nullable: true),
                    CreatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusHabit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.IdHabit);
                    table.ForeignKey(
                        name: "FK_Habits_Projects_ProjectIdProject",
                        column: x => x.ProjectIdProject,
                        principalTable: "Projects",
                        principalColumn: "IdProject");
                });

            migrationBuilder.CreateTable(
                name: "PullGroups",
                columns: table => new
                {
                    IdPull = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PullDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProjectIdProject = table.Column<int>(type: "int", nullable: true),
                    CreatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusPull = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PullGroups", x => x.IdPull);
                    table.ForeignKey(
                        name: "FK_PullGroups_Projects_ProjectIdProject",
                        column: x => x.ProjectIdProject,
                        principalTable: "Projects",
                        principalColumn: "IdProject");
                });

            migrationBuilder.CreateTable(
                name: "PushesGroups",
                columns: table => new
                {
                    IdPush = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PushName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PushDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProjectIdProject = table.Column<int>(type: "int", nullable: true),
                    CreatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusPush = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushesGroups", x => x.IdPush);
                    table.ForeignKey(
                        name: "FK_PushesGroups_Projects_ProjectIdProject",
                        column: x => x.ProjectIdProject,
                        principalTable: "Projects",
                        principalColumn: "IdProject");
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    IdStorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectIdProject = table.Column<int>(type: "int", nullable: true),
                    TitleStorie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContextStorie = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    IdInter1 = table.Column<int>(type: "int", nullable: true),
                    CreatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.IdStorie);
                    table.ForeignKey(
                        name: "FK_Stories_Interviews_IdInter1",
                        column: x => x.IdInter1,
                        principalTable: "Interviews",
                        principalColumn: "IdInter");
                    table.ForeignKey(
                        name: "FK_Stories_Projects_ProjectIdProject",
                        column: x => x.ProjectIdProject,
                        principalTable: "Projects",
                        principalColumn: "IdProject");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anxieties_ProjectIdProject",
                table: "Anxieties",
                column: "ProjectIdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeparmentsId",
                table: "Employees",
                column: "DeparmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Habits_ProjectIdProject",
                table: "Habits",
                column: "ProjectIdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CategoriesId",
                table: "Projects",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeparmentId",
                table: "Projects",
                column: "DeparmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PullGroups_ProjectIdProject",
                table: "PullGroups",
                column: "ProjectIdProject");

            migrationBuilder.CreateIndex(
                name: "IX_PushesGroups_ProjectIdProject",
                table: "PushesGroups",
                column: "ProjectIdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_IdInter1",
                table: "Stories",
                column: "IdInter1");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ProjectIdProject",
                table: "Stories",
                column: "ProjectIdProject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anxieties");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Habits");

            migrationBuilder.DropTable(
                name: "PullGroups");

            migrationBuilder.DropTable(
                name: "PushesGroups");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Deparments");
        }
    }
}
