using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueColumnsForSoftDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "UK_Users_Email", table: "Users");

            migrationBuilder.DropIndex(name: "UK_UserOperationClaims_UserId_OperationClaimId", table: "UserOperationClaims");

            migrationBuilder.DropIndex(name: "UK_OperationClaims_Name", table: "OperationClaims");

            migrationBuilder.DropColumn(name: "Created", table: "RefreshTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OperationClaims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)"
            );

            migrationBuilder.CreateIndex(name: "IX_UserOperationClaims_UserId", table: "UserOperationClaims", column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_UserOperationClaims_UserId", table: "UserOperationClaims");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OperationClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );

            migrationBuilder.CreateIndex(name: "UK_Users_Email", table: "Users", column: "Email", unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_UserOperationClaims_UserId_OperationClaimId",
                table: "UserOperationClaims",
                columns: new[] { "UserId", "OperationClaimId" },
                unique: true
            );

            migrationBuilder.CreateIndex(name: "UK_OperationClaims_Name", table: "OperationClaims", column: "Name", unique: true);
        }
    }
}
