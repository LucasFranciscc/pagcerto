using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nsu = table.Column<int>(nullable: false),
                    TransactionDatePerformed = table.Column<DateTime>(nullable: false),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    FailureDate = table.Column<DateTime>(nullable: true),
                    Anticipated = table.Column<bool>(nullable: true),
                    AcquirerConfirmation = table.Column<string>(nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    FlatRate = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NumberOfInstallments = table.Column<int>(nullable: false),
                    CardFinal = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
