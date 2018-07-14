using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_BillsPaymentSystem.Data.Migrations
{
    public partial class ValidationAttributsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymenMethodId",
                table: "BankAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "PaymenMethodId",
                table: "BankAccounts");
        }
    }
}
