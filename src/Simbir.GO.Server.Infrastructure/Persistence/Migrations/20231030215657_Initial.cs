using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Domain.Rents.Enums;
using Simbir.GO.Server.Domain.Transports.Enums;

#nullable disable

namespace Simbir.GO.Server.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:price_type", "none,minutes,days")
                .Annotation("Npgsql:Enum:role", "none,customer,admin")
                .Annotation("Npgsql:Enum:transport_type", "none,car,bike,scooter");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: false),
                    passwordSalt = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<Role>(type: "role", nullable: false),
                    balance = table.Column<double>(type: "double precision", nullable: false),
                    createdDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transports",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transportOwnerId = table.Column<long>(type: "bigint", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false),
                    identifier = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    location_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    location_Longitude = table.Column<double>(type: "double precision", nullable: false),
                    minutePrice = table.Column<double>(type: "double precision", nullable: true),
                    dayPrice = table.Column<double>(type: "double precision", nullable: true),
                    canBeRented = table.Column<bool>(type: "boolean", nullable: false),
                    transportType = table.Column<TransportType>(type: "transport_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_transports", x => x.id);
                    table.ForeignKey(
                        name: "fK_transports_accounts_AccountTempId1",
                        column: x => x.transportOwnerId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rents",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transportId = table.Column<long>(type: "bigint", nullable: false),
                    accountId = table.Column<long>(type: "bigint", nullable: false),
                    priceType = table.Column<PriceType>(type: "price_type", nullable: false),
                    timeStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    timeEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    priceOfUnit = table.Column<double>(type: "double precision", nullable: false),
                    finalPrice = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_rents", x => x.id);
                    table.ForeignKey(
                        name: "fK_rents_accounts_AccountTempId",
                        column: x => x.accountId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_rents_transports_TransportTempId",
                        column: x => x.transportId,
                        principalTable: "transports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_rents_accountId",
                table: "rents",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "iX_rents_transportId",
                table: "rents",
                column: "transportId");

            migrationBuilder.CreateIndex(
                name: "iX_transports_transportOwnerId",
                table: "transports",
                column: "transportOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rents");

            migrationBuilder.DropTable(
                name: "transports");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
