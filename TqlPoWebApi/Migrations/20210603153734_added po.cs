﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TqlPoWebApi.Migrations
{
    public partial class addedpo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 80, nullable: false),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pos_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pos_EmployeeId",
                table: "Pos",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pos");
        }
    }
}
