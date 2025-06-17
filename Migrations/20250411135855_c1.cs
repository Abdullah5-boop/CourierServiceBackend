using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courier_Service_part_1.Migrations
{
    /// <inheritdoc />
    public partial class c1 : Migration
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
                name: "reciver_table",
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
                    table.PrimaryKey("PK_reciver_table", x => x.reciver_id);
                });

            migrationBuilder.CreateTable(
                name: "sender_table",
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
                    table.PrimaryKey("PK_sender_table", x => x.sender_id);
                });

            migrationBuilder.CreateTable(
                name: "order_Track",
                columns: table => new
                {
                    Order_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    reciver_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    sender_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_Track", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_order_Track_reciver_table_reciver_id",
                        column: x => x.reciver_id,
                        principalTable: "reciver_table",
                        principalColumn: "reciver_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_Track_sender_table_sender_id",
                        column: x => x.sender_id,
                        principalTable: "sender_table",
                        principalColumn: "sender_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_table",
                columns: table => new
                {
                    order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_track_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delivery_man_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_price = table.Column<double>(type: "float", nullable: false),
                    order_statuss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_wigth = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_table", x => x.order_Id);
                    table.ForeignKey(
                        name: "FK_order_table_delivery_Man_table_delivery_man_id",
                        column: x => x.delivery_man_id,
                        principalTable: "delivery_Man_table",
                        principalColumn: "d_m_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_table_order_Track_order_track_id",
                        column: x => x.order_track_id,
                        principalTable: "order_Track",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_table_delivery_man_id",
                table: "order_table",
                column: "delivery_man_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_table_order_track_id",
                table: "order_table",
                column: "order_track_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_Track_reciver_id",
                table: "order_Track",
                column: "reciver_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_Track_sender_id",
                table: "order_Track",
                column: "sender_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_table");

            migrationBuilder.DropTable(
                name: "delivery_Man_table");

            migrationBuilder.DropTable(
                name: "order_Track");

            migrationBuilder.DropTable(
                name: "reciver_table");

            migrationBuilder.DropTable(
                name: "sender_table");
        }
    }
}
