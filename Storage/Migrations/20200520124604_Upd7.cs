using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace laba5_oop.Storage.Migrations
{
    public partial class Upd7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdBrand",
                table: "tblDetail",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BrandOfDetails",
                columns: table => new
                {
                    IdBrand = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandOfDetails", x => x.IdBrand);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblDetail_IdBrand",
                table: "tblDetail",
                column: "IdBrand");

            migrationBuilder.AddForeignKey(
                name: "FK_tblDetail_BrandOfDetails_IdBrand",
                table: "tblDetail",
                column: "IdBrand",
                principalTable: "BrandOfDetails",
                principalColumn: "IdBrand",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblDetail_BrandOfDetails_IdBrand",
                table: "tblDetail");

            migrationBuilder.DropTable(
                name: "BrandOfDetails");

            migrationBuilder.DropIndex(
                name: "IX_tblDetail_IdBrand",
                table: "tblDetail");

            migrationBuilder.DropColumn(
                name: "IdBrand",
                table: "tblDetail");
        }
    }
}
