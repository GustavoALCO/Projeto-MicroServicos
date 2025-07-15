using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdsService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUser = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Category_CategoryType = table.Column<string>(type: "text", nullable: false),
                    Category_Car_CarBrand = table.Column<string>(type: "text", nullable: true),
                    Category_Car_Model = table.Column<string>(type: "text", nullable: true),
                    Category_Car_Type = table.Column<string>(type: "text", nullable: true),
                    Category_Car_Year = table.Column<string>(type: "text", nullable: true),
                    Category_Car_KM = table.Column<int>(type: "integer", nullable: true),
                    Category_Car_Engine = table.Column<decimal>(type: "numeric", nullable: true),
                    Category_Car_Fuel = table.Column<string>(type: "text", nullable: true),
                    Category_Car_Gearbox = table.Column<bool>(type: "boolean", nullable: true),
                    Category_Car_Color = table.Column<string>(type: "text", nullable: true),
                    Category_House_House = table.Column<string>(type: "text", nullable: true),
                    Category_House_ZIPCode = table.Column<string>(type: "text", nullable: true),
                    Category_House_Bedroom = table.Column<int>(type: "integer", nullable: true),
                    Category_House_Beathroom = table.Column<int>(type: "integer", nullable: true),
                    Category_House_CarSpace = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.IdProduct);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ProductIdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdImage = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => new { x.ProductIdProduct, x.Id });
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductIdProduct",
                        column: x => x.ProductIdProduct,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
