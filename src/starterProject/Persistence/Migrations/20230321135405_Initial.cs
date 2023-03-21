using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table =>
                    new
                    {
                        Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table =>
                    new
                    {
                        Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                        PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                        Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                        AuthenticatorType = table.Column<int>(type: "int", nullable: false),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "EmailAuthenticators",
                columns: table =>
                    new
                    {
                        Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<int>(type: "int", nullable: false),
                        ActivationKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        IsVerified = table.Column<bool>(type: "bit", nullable: false),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAuthenticators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "OtpAuthenticators",
                columns: table =>
                    new
                    {
                        Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<int>(type: "int", nullable: false),
                        SecretKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                        IsVerified = table.Column<bool>(type: "bit", nullable: false),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtpAuthenticators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table =>
                    new
                    {
                        Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<int>(type: "int", nullable: false),
                        Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                        CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                        RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table =>
                    new
                    {
                        Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<int>(type: "int", nullable: false),
                        OperationClaimId = table.Column<int>(type: "int", nullable: false),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", null }
            );

            migrationBuilder.CreateIndex(name: "IX_EmailAuthenticators_UserId", table: "EmailAuthenticators", column: "UserId");

            migrationBuilder.CreateIndex(name: "UK_OperationClaims_Name", table: "OperationClaims", column: "Name", unique: true);

            migrationBuilder.CreateIndex(name: "IX_OtpAuthenticators_UserId", table: "OtpAuthenticators", column: "UserId");

            migrationBuilder.CreateIndex(name: "IX_RefreshTokens_UserId", table: "RefreshTokens", column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId"
            );

            migrationBuilder.CreateIndex(
                name: "UK_UserOperationClaims_UserId_OperationClaimId",
                table: "UserOperationClaims",
                columns: new[] { "UserId", "OperationClaimId" },
                unique: true
            );

            migrationBuilder.CreateIndex(name: "UK_Users_Email", table: "Users", column: "Email", unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "EmailAuthenticators");

            migrationBuilder.DropTable(name: "OtpAuthenticators");

            migrationBuilder.DropTable(name: "RefreshTokens");

            migrationBuilder.DropTable(name: "UserOperationClaims");

            migrationBuilder.DropTable(name: "OperationClaims");

            migrationBuilder.DropTable(name: "Users");
        }
    }
}
