using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BookStore.Migrations
{
    public partial class AddDebtRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountAfterChanged",
                table: "BookAmountChangingRecord",
                newName: "AmountBeforeChanged");

            migrationBuilder.RenameColumn(
                name: "ChangeNumber",
                table: "BookAmountChangingRecord",
                newName: "RecordId");

            migrationBuilder.CreateTable(
                name: "CustomerDebtChangingRecord",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<int>(type: "integer", nullable: true),
                    IsCollectMoney = table.Column<bool>(type: "boolean", nullable: false),
                    BeforeChangeAmount = table.Column<int>(type: "integer", nullable: false),
                    ChangeMoneyAmount = table.Column<int>(type: "integer", nullable: false),
                    DateChanged = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDebtChangingRecord", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_CustomerDebtChangingRecord_Customers_BookId",
                        column: x => x.BookId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDebtChangingRecord_BookId",
                table: "CustomerDebtChangingRecord",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDebtChangingRecord");

            migrationBuilder.RenameColumn(
                name: "AmountBeforeChanged",
                table: "BookAmountChangingRecord",
                newName: "AmountAfterChanged");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "BookAmountChangingRecord",
                newName: "ChangeNumber");
        }
    }
}
