using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LoginCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LoginCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OrderStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentModes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PaymentModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleType = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_SubCategories_tbl_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "tbl_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserDetails",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserDetails", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_tbl_UserDetails_tbl_LoginCredentials_UserID",
                        column: x => x.UserID,
                        principalTable: "tbl_LoginCredentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_UserDetails_tbl_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "tbl_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Brands_tbl_SubCategories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "tbl_SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    PinCode = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DeliveryAddresses_tbl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "tbl_UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_RefreshTokens",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RefreshTokens", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_tbl_RefreshTokens_tbl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "tbl_UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Products_tbl_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "tbl_Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DeliveryAddID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentModeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OrderStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_OrderDetails_tbl_DeliveryAddresses_DeliveryAddID",
                        column: x => x.DeliveryAddID,
                        principalTable: "tbl_DeliveryAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_OrderDetails_tbl_OrderStatus_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalTable: "tbl_OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_OrderDetails_tbl_PaymentModes_PaymentModeID",
                        column: x => x.PaymentModeID,
                        principalTable: "tbl_PaymentModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_OrderDetails_tbl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "tbl_UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Models",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Models_tbl_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tbl_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CartItems",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CartItems", x => new { x.UserID, x.ModelID });
                    table.ForeignKey(
                        name: "FK_tbl_CartItems_tbl_Models_ModelID",
                        column: x => x.ModelID,
                        principalTable: "tbl_Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_CartItems_tbl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "tbl_UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OrderItems",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OrderItems", x => new { x.OrderID, x.ModelID });
                    table.ForeignKey(
                        name: "FK_tbl_OrderItems_tbl_Models_ModelID",
                        column: x => x.ModelID,
                        principalTable: "tbl_Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OrderItems_tbl_OrderDetails_OrderID",
                        column: x => x.OrderID,
                        principalTable: "tbl_OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Brands_SubCategoryID",
                table: "tbl_Brands",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CartItems_ModelID",
                table: "tbl_CartItems",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DeliveryAddresses_UserID",
                table: "tbl_DeliveryAddresses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_LoginCredentials_EmailID",
                table: "tbl_LoginCredentials",
                column: "EmailID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Models_ProductID",
                table: "tbl_Models",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderDetails_DeliveryAddID",
                table: "tbl_OrderDetails",
                column: "DeliveryAddID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderDetails_OrderStatusID",
                table: "tbl_OrderDetails",
                column: "OrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderDetails_PaymentModeID",
                table: "tbl_OrderDetails",
                column: "PaymentModeID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderDetails_UserID",
                table: "tbl_OrderDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderItems_ModelID",
                table: "tbl_OrderItems",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Products_BrandID",
                table: "tbl_Products",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SubCategories_CategoryID",
                table: "tbl_SubCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserDetails_RoleID",
                table: "tbl_UserDetails",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_CartItems");

            migrationBuilder.DropTable(
                name: "tbl_OrderItems");

            migrationBuilder.DropTable(
                name: "tbl_RefreshTokens");

            migrationBuilder.DropTable(
                name: "tbl_Models");

            migrationBuilder.DropTable(
                name: "tbl_OrderDetails");

            migrationBuilder.DropTable(
                name: "tbl_Products");

            migrationBuilder.DropTable(
                name: "tbl_DeliveryAddresses");

            migrationBuilder.DropTable(
                name: "tbl_OrderStatus");

            migrationBuilder.DropTable(
                name: "tbl_PaymentModes");

            migrationBuilder.DropTable(
                name: "tbl_Brands");

            migrationBuilder.DropTable(
                name: "tbl_UserDetails");

            migrationBuilder.DropTable(
                name: "tbl_SubCategories");

            migrationBuilder.DropTable(
                name: "tbl_LoginCredentials");

            migrationBuilder.DropTable(
                name: "tbl_Roles");

            migrationBuilder.DropTable(
                name: "tbl_Categories");
        }
    }
}
