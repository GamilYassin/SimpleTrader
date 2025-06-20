using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleTrader.WPF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MajorIndex",
                table: "MajorIndex");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MajorIndex");

            migrationBuilder.RenameTable(
                name: "MajorIndex",
                newName: "MajorIndices");

            migrationBuilder.RenameColumn(
                name: "Asset_Symbol",
                table: "AssetTransactions",
                newName: "Symbol");

            migrationBuilder.RenameColumn(
                name: "Asset_PricePerShare",
                table: "AssetTransactions",
                newName: "PricePerShare");

            migrationBuilder.RenameIndex(
                name: "IX_MajorIndex_IndexName",
                table: "MajorIndices",
                newName: "IX_MajorIndices_IndexName");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerShare",
                table: "AssetTransactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MajorIndices",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "Changes",
                table: "MajorIndices",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "UriSuffix",
                table: "MajorIndices",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MajorIndices",
                table: "MajorIndices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PricePerShare = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MajorIndices",
                table: "MajorIndices");

            migrationBuilder.DropColumn(
                name: "UriSuffix",
                table: "MajorIndices");

            migrationBuilder.RenameTable(
                name: "MajorIndices",
                newName: "MajorIndex");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "AssetTransactions",
                newName: "Asset_Symbol");

            migrationBuilder.RenameColumn(
                name: "PricePerShare",
                table: "AssetTransactions",
                newName: "Asset_PricePerShare");

            migrationBuilder.RenameIndex(
                name: "IX_MajorIndices_IndexName",
                table: "MajorIndex",
                newName: "IX_MajorIndex_IndexName");

            migrationBuilder.AlterColumn<double>(
                name: "Asset_PricePerShare",
                table: "AssetTransactions",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "Accounts",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "MajorIndex",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Changes",
                table: "MajorIndex",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MajorIndex",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MajorIndex",
                table: "MajorIndex",
                column: "Id");
        }
    }
}
