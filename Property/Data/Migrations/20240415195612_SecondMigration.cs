using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubcategoriesRealEstate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoriesRealEstate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubcategoriesRealEstate_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsRealEstate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Room = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfConstruction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    SubcategoryRealEstateId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsRealEstate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsRealEstate_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsRealEstate_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsRealEstate_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsRealEstate_SubcategoriesRealEstate_SubcategoryRealEstateId",
                        column: x => x.SubcategoryRealEstateId,
                        principalTable: "SubcategoriesRealEstate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productImagesRealEstate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductRealEstateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productImagesRealEstate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productImagesRealEstate_ProductsRealEstate_ProductRealEstateId",
                        column: x => x.ProductRealEstateId,
                        principalTable: "ProductsRealEstate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentsRealEstate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductRealEstateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentsRealEstate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentsRealEstate_ProductsRealEstate_ProductRealEstateId",
                        column: x => x.ProductRealEstateId,
                        principalTable: "ProductsRealEstate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sellsRealEstate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductRealEstateId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellsRealEstate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sellsRealEstate_ProductsRealEstate_ProductRealEstateId",
                        column: x => x.ProductRealEstateId,
                        principalTable: "ProductsRealEstate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rentsRealEstatePerDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentRealEstateId = table.Column<int>(type: "int", nullable: false),
                    NumberOfPoeple = table.Column<int>(type: "int", nullable: false),
                    AdditionalPerson = table.Column<int>(type: "int", nullable: false),
                    AdditionalPrice = table.Column<double>(type: "float", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    Penality = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentsRealEstatePerDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rentsRealEstatePerDay_RentsRealEstate_RentRealEstateId",
                        column: x => x.RentRealEstateId,
                        principalTable: "RentsRealEstate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rentsRealEstatePerMounth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentRealEstateId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentsRealEstatePerMounth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rentsRealEstatePerMounth_RentsRealEstate_RentRealEstateId",
                        column: x => x.RentRealEstateId,
                        principalTable: "RentsRealEstate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ApplicationUserId",
                table: "Agents",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentId",
                table: "Countries",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_productImagesRealEstate_ProductRealEstateId",
                table: "productImagesRealEstate",
                column: "ProductRealEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsRealEstate_AgentId",
                table: "ProductsRealEstate",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsRealEstate_CityId",
                table: "ProductsRealEstate",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsRealEstate_CountryId",
                table: "ProductsRealEstate",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsRealEstate_SubcategoryRealEstateId",
                table: "ProductsRealEstate",
                column: "SubcategoryRealEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RentsRealEstate_ProductRealEstateId",
                table: "RentsRealEstate",
                column: "ProductRealEstateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rentsRealEstatePerDay_RentRealEstateId",
                table: "rentsRealEstatePerDay",
                column: "RentRealEstateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rentsRealEstatePerMounth_RentRealEstateId",
                table: "rentsRealEstatePerMounth",
                column: "RentRealEstateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sellsRealEstate_ProductRealEstateId",
                table: "sellsRealEstate",
                column: "ProductRealEstateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubcategoriesRealEstate_CategoryId",
                table: "SubcategoriesRealEstate",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productImagesRealEstate");

            migrationBuilder.DropTable(
                name: "rentsRealEstatePerDay");

            migrationBuilder.DropTable(
                name: "rentsRealEstatePerMounth");

            migrationBuilder.DropTable(
                name: "sellsRealEstate");

            migrationBuilder.DropTable(
                name: "RentsRealEstate");

            migrationBuilder.DropTable(
                name: "ProductsRealEstate");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "SubcategoriesRealEstate");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Continents");
        }
    }
}
