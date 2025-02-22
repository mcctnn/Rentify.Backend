using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b7f857bb-de17-4022-8ccd-8dd334dcd915"),
                column: "UserRole",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("be967297-21d2-4767-ae1b-c79b93f36950"),
                column: "UserRole",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cda48d5e-987c-496f-9731-7934f63d598a"),
                column: "UserRole",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b7f857bb-de17-4022-8ccd-8dd334dcd915"),
                column: "UserRole",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("be967297-21d2-4767-ae1b-c79b93f36950"),
                column: "UserRole",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cda48d5e-987c-496f-9731-7934f63d598a"),
                column: "UserRole",
                value: 0);
        }
    }
}
