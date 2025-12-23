using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shipping");

            migrationBuilder.CreateSequence(
                name: "shipmentseq",
                schema: "shipping",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "shipmentstatushistoryseq",
                schema: "shipping",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "shipmentwaypointseq",
                schema: "shipping",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "shipperseq",
                schema: "shipping",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                schema: "shipping",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventTypeName = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TimesSent = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                schema: "shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    CurrentWarehouseId = table.Column<int>(type: "integer", nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                schema: "shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ShipperId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CustomerAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CustomerCity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CustomerCountry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalSchema: "shipping",
                        principalTable: "Shippers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShipmentWaypoints",
                schema: "shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ShipmentId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    ArrivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DepartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentWaypoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentWaypoints_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalSchema: "shipping",
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentStatusHistory",
                schema: "shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ShipmentId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WaypointId = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentStatusHistory_ShipmentWaypoints_WaypointId",
                        column: x => x.WaypointId,
                        principalSchema: "shipping",
                        principalTable: "ShipmentWaypoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShipmentStatusHistory_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalSchema: "shipping",
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                schema: "shipping",
                table: "Shipments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipperId",
                schema: "shipping",
                table: "Shipments",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatusHistory_ShipmentId",
                schema: "shipping",
                table: "ShipmentStatusHistory",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatusHistory_WaypointId",
                schema: "shipping",
                table: "ShipmentStatusHistory",
                column: "WaypointId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentWaypoints_ShipmentId_Sequence",
                schema: "shipping",
                table: "ShipmentWaypoints",
                columns: new[] { "ShipmentId", "Sequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_UserId",
                schema: "shipping",
                table: "Shippers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "shipping");

            migrationBuilder.DropTable(
                name: "ShipmentStatusHistory",
                schema: "shipping");

            migrationBuilder.DropTable(
                name: "ShipmentWaypoints",
                schema: "shipping");

            migrationBuilder.DropTable(
                name: "Shipments",
                schema: "shipping");

            migrationBuilder.DropTable(
                name: "Shippers",
                schema: "shipping");

            migrationBuilder.DropSequence(
                name: "shipmentseq",
                schema: "shipping");

            migrationBuilder.DropSequence(
                name: "shipmentstatushistoryseq",
                schema: "shipping");

            migrationBuilder.DropSequence(
                name: "shipmentwaypointseq",
                schema: "shipping");

            migrationBuilder.DropSequence(
                name: "shipperseq",
                schema: "shipping");
        }
    }
}
