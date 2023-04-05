using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(name: "DeletedDate", table: "Users", type: "datetime2", nullable: true);

            migrationBuilder.AddColumn<DateTime>(name: "DeletedDate", table: "UserOperationClaims", type: "datetime2", nullable: true);

            migrationBuilder.AddColumn<DateTime>(name: "DeletedDate", table: "RefreshTokens", type: "datetime2", nullable: true);

            migrationBuilder.AddColumn<DateTime>(name: "DeletedDate", table: "OtpAuthenticators", type: "datetime2", nullable: true);

            migrationBuilder.AddColumn<DateTime>(name: "DeletedDate", table: "OperationClaims", type: "datetime2", nullable: true);

            migrationBuilder.AddColumn<DateTime>(name: "DeletedDate", table: "EmailAuthenticators", type: "datetime2", nullable: true);

            migrationBuilder.UpdateData(table: "OperationClaims", keyColumn: "Id", keyValue: 1, column: "DeletedDate", value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "DeletedDate", table: "Users");

            migrationBuilder.DropColumn(name: "DeletedDate", table: "UserOperationClaims");

            migrationBuilder.DropColumn(name: "DeletedDate", table: "RefreshTokens");

            migrationBuilder.DropColumn(name: "DeletedDate", table: "OtpAuthenticators");

            migrationBuilder.DropColumn(name: "DeletedDate", table: "OperationClaims");

            migrationBuilder.DropColumn(name: "DeletedDate", table: "EmailAuthenticators");
        }
    }
}
