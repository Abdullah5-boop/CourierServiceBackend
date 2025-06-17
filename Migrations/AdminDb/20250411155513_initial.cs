using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courier_Service_part_1.Migrations.AdminDb
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "delivery_Man_table",
                columns: table => new
                {
                    d_m_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    d_m_admin_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delivery_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    d_m_status_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_Man_table", x => x.d_m_id);
                });

            migrationBuilder.CreateTable(
                name: "ReciverClass",
                columns: table => new
                {
                    reciver_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zip_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    personType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unique_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unique_phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReciverClass", x => x.reciver_id);
                });

            migrationBuilder.CreateTable(
                name: "SenderClass",
                columns: table => new
                {
                    sender_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zip_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    personType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unique_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unique_phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenderClass", x => x.sender_id);
                });

            migrationBuilder.CreateTable(
                name: "Order_track_Class",
                columns: table => new
                {
                    Order_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    reciver_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sender_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sender_id1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    reciver_id1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_track_Class", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Order_track_Class_ReciverClass_reciver_id1",
                        column: x => x.reciver_id1,
                        principalTable: "ReciverClass",
                        principalColumn: "reciver_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_track_Class_SenderClass_sender_id1",
                        column: x => x.sender_id1,
                        principalTable: "SenderClass",
                        principalColumn: "sender_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderClass",
                columns: table => new
                {
                    order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_track_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delivery_man_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_price = table.Column<double>(type: "float", nullable: false),
                    order_statuss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_wigth = table.Column<double>(type: "float", nullable: false),
                    order_TrackOrder_Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderClass", x => x.order_Id);
                    table.ForeignKey(
                        name: "FK_OrderClass_Order_track_Class_order_TrackOrder_Id",
                        column: x => x.order_TrackOrder_Id,
                        principalTable: "Order_track_Class",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderClass_delivery_Man_table_delivery_man_id",
                        column: x => x.delivery_man_id,
                        principalTable: "delivery_Man_table",
                        principalColumn: "d_m_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_track_Class_reciver_id1",
                table: "Order_track_Class",
                column: "reciver_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Order_track_Class_sender_id1",
                table: "Order_track_Class",
                column: "sender_id1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderClass_delivery_man_id",
                table: "OrderClass",
                column: "delivery_man_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderClass_order_TrackOrder_Id",
                table: "OrderClass",
                column: "order_TrackOrder_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderClass");

            migrationBuilder.DropTable(
                name: "Order_track_Class");

            migrationBuilder.DropTable(
                name: "delivery_Man_table");

            migrationBuilder.DropTable(
                name: "ReciverClass");

            migrationBuilder.DropTable(
                name: "SenderClass");
        }
    }
}
