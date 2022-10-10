using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class migration_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 10, 19, 45, 56, 433, DateTimeKind.Local).AddTicks(9353),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 10, 19, 43, 42, 748, DateTimeKind.Local).AddTicks(8533));

            migrationBuilder.InsertData(
                table: "CorporateCustomers",
                columns: new[] { "Id", "CompanyName", "CustomerId", "TaxNo" },
                values: new object[] { 1, "Ahmet Çetinkaya", 2, "54154512" });

            migrationBuilder.InsertData(
                table: "FindeksCreditRates",
                columns: new[] { "Id", "CustomerId", "Score" },
                values: new object[,]
                {
                    { 1, 1, (short)1000 },
                    { 2, 2, (short)1900 }
                });

            migrationBuilder.InsertData(
                table: "IndividualCustomers",
                columns: new[] { "Id", "CustomerId", "FirstName", "LastName", "NationalIdentity" },
                values: new object[] { 1, 1, "Ahmet", "Çetinkaya", "123123123123" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "CreatedDate", "CustomerId", "No", "RentalEndDate", "RentalPrice", "RentalStartDate", "TotalRentalDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 1, "123123", new DateTime(2022, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 1000m, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), (short)2 },
                    { 2, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 1, "123123", new DateTime(2022, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 2000m, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), (short)2 }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "CarId", "CustomerId", "RentEndDate", "RentEndKilometer", "RentStartDate", "RentStartKilometer", "ReturnDate" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTime(2022, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 1200, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 1000, null },
                    { 2, 1, 2, new DateTime(2022, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 1200, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 1000, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CorporateCustomers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FindeksCreditRates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FindeksCreditRates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IndividualCustomers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 10, 19, 43, 42, 748, DateTimeKind.Local).AddTicks(8533),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 10, 19, 45, 56, 433, DateTimeKind.Local).AddTicks(9353));
        }
    }
}
