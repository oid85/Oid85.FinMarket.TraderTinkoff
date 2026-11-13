using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _13112026_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "public",
                table: "InstrumentEntities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Lot",
                schema: "public",
                table: "InstrumentEntities",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "public",
                table: "InstrumentEntities");

            migrationBuilder.DropColumn(
                name: "Lot",
                schema: "public",
                table: "InstrumentEntities");
        }
    }
}
