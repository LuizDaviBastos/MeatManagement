using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeatManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Document = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Meats_MeatId",
                        column: x => x.MeatId,
                        principalTable: "Meats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Name", "UF" },
                values: new object[,]
                {
                    { new Guid("1e3f5a1b-2d4c-4f8a-9e2a-1234567890ab"), "São Paulo", "SP" },
                    { new Guid("2b4c6d7e-3f8a-4b2c-9f3d-2345678901bc"), "Rio de Janeiro", "RJ" },
                    { new Guid("3c5d7e8f-4a1b-5c3d-0a4e-3456789012cd"), "Minas Gerais", "MG" },
                    { new Guid("4d6e7f8a-1b2c-3d4e-5f6a-4567890123ee"), "Paraná", "PR" },
                    { new Guid("5e7f8a9b-2c3d-4e5f-6a7b-5678901234ff"), "Rio Grande do Sul", "RS" },
                    { new Guid("6f8a9b0c-3d4e-5f6a-7b8c-6789012345aa"), "Bahia", "BA" },
                    { new Guid("7a9b0c1d-4e5f-6a7b-8c9d-7890123456bb"), "Distrito Federal", "DF" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "São Paulo", new Guid("1e3f5a1b-2d4c-4f8a-9e2a-1234567890ab") },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Campinas", new Guid("1e3f5a1b-2d4c-4f8a-9e2a-1234567890ab") },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Santos", new Guid("1e3f5a1b-2d4c-4f8a-9e2a-1234567890ab") },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Rio de Janeiro", new Guid("2b4c6d7e-3f8a-4b2c-9f3d-2345678901bc") },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Niterói", new Guid("2b4c6d7e-3f8a-4b2c-9f3d-2345678901bc") },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "Petrópolis", new Guid("2b4c6d7e-3f8a-4b2c-9f3d-2345678901bc") },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "Belo Horizonte", new Guid("3c5d7e8f-4a1b-5c3d-0a4e-3456789012cd") },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "Uberlândia", new Guid("3c5d7e8f-4a1b-5c3d-0a4e-3456789012cd") },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "Ouro Preto", new Guid("3c5d7e8f-4a1b-5c3d-0a4e-3456789012cd") },
                    { new Guid("10000000-0000-0000-0000-000000000010"), "Curitiba", new Guid("4d6e7f8a-1b2c-3d4e-5f6a-4567890123ee") },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "Londrina", new Guid("4d6e7f8a-1b2c-3d4e-5f6a-4567890123ee") },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "Porto Alegre", new Guid("5e7f8a9b-2c3d-4e5f-6a7b-5678901234ff") },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "Caxias do Sul", new Guid("5e7f8a9b-2c3d-4e5f-6a7b-5678901234ff") },
                    { new Guid("10000000-0000-0000-0000-000000000014"), "Salvador", new Guid("6f8a9b0c-3d4e-5f6a-7b8c-6789012345aa") },
                    { new Guid("10000000-0000-0000-0000-000000000015"), "Feira de Santana", new Guid("6f8a9b0c-3d4e-5f6a-7b8c-6789012345aa") },
                    { new Guid("10000000-0000-0000-0000-000000000016"), "Brasília", new Guid("7a9b0c1d-4e5f-6a7b-8c9d-7890123456bb") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_BuyerId",
                table: "Address",
                column: "BuyerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StateId",
                table: "Address",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_Document",
                table: "Buyers",
                column: "Document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_MeatId",
                table: "OrderItem",
                column: "MeatId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Meats");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Buyers");
        }
    }
}
