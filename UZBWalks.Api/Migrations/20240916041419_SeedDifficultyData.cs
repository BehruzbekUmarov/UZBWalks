using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UZBWalks.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedDifficultyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8255bba3-7613-4f1d-9d2d-3fa1da0f4117"), "Easy" },
                    { new Guid("d86b62d0-a0ff-4cca-b747-8104e23a2af7"), "Medium" },
                    { new Guid("fa0ea5c5-66ef-4f2d-985f-4606f40cda49"), "Hard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8255bba3-7613-4f1d-9d2d-3fa1da0f4117"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d86b62d0-a0ff-4cca-b747-8104e23a2af7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("fa0ea5c5-66ef-4f2d-985f-4606f40cda49"));
        }
    }
}
