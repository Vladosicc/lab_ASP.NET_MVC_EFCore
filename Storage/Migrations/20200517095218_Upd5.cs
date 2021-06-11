using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace laba5_oop.Storage.Migrations
{
    public partial class Upd5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "ModelCars",
                columns: table => new
                {
                    IdCarModel = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelCars", x => x.IdCarModel);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(nullable: false),
                    decPrice = table.Column<decimal>(nullable: false),
                    szDetailsId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.IdOrder);
                });

            migrationBuilder.CreateTable(
                name: "CarForSolds",
                columns: table => new
                {
                    IdCar = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(nullable: false),
                    intMileage = table.Column<int>(nullable: false),
                    decPrice = table.Column<decimal>(nullable: false),
                    IdCarModel = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarForSolds", x => x.IdCar);
                    table.ForeignKey(
                        name: "FK_CarForSolds_ModelCars_IdCarModel",
                        column: x => x.IdCarModel,
                        principalTable: "ModelCars",
                        principalColumn: "IdCarModel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblDetail",
                columns: table => new
                {
                    IdDetail = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(nullable: false),
                    decPrice = table.Column<decimal>(nullable: false),
                    IdCategory = table.Column<Guid>(nullable: false),
                    IdCarModel = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDetail", x => x.IdDetail);
                    table.ForeignKey(
                        name: "FK_tblDetail_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblDetail_ModelCars_IdCarModel",
                        column: x => x.IdCarModel,
                        principalTable: "ModelCars",
                        principalColumn: "IdCarModel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarForSolds_IdCarModel",
                table: "CarForSolds",
                column: "IdCarModel");

            migrationBuilder.CreateIndex(
                name: "IX_tblDetail_IdCategory",
                table: "tblDetail",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_tblDetail_IdCarModel",
                table: "tblDetail",
                column: "IdCarModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarForSolds");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "tblDetail");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ModelCars");
        }
    }
}
