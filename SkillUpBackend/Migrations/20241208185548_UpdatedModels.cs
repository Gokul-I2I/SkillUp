using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillUpBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertedBy = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(25)", nullable: true),
                    InsertedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Qualification = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(70)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertedBy = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(25)", nullable: true),
                    InsertedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
