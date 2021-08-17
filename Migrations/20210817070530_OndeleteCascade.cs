using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class OndeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillsDetails_Bills_BillId",
                table: "BillsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillsDetails_Books_BookId",
                table: "BillsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAmountChangingRecord_Books_BookId",
                table: "BookAmountChangingRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookTypes_TypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Customers_CustomerId",
                table: "Receipts");

            migrationBuilder.AddForeignKey(
                name: "FK_BillsDetails_Bills_BillId",
                table: "BillsDetails",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillsDetails_Books_BookId",
                table: "BillsDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAmountChangingRecord_Books_BookId",
                table: "BookAmountChangingRecord",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookTypes_TypeId",
                table: "Books",
                column: "TypeId",
                principalTable: "BookTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Customers_CustomerId",
                table: "Receipts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillsDetails_Bills_BillId",
                table: "BillsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillsDetails_Books_BookId",
                table: "BillsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAmountChangingRecord_Books_BookId",
                table: "BookAmountChangingRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookTypes_TypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Customers_CustomerId",
                table: "Receipts");

            migrationBuilder.AddForeignKey(
                name: "FK_BillsDetails_Bills_BillId",
                table: "BillsDetails",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillsDetails_Books_BookId",
                table: "BillsDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAmountChangingRecord_Books_BookId",
                table: "BookAmountChangingRecord",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookTypes_TypeId",
                table: "Books",
                column: "TypeId",
                principalTable: "BookTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Customers_CustomerId",
                table: "Receipts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
