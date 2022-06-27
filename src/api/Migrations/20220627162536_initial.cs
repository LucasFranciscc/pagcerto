using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anticipation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UniqueIdentifier = table.Column<int>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    AnalysisStartDate = table.Column<DateTime>(nullable: true),
                    AnalysisEndDate = table.Column<DateTime>(nullable: true),
                    AnalysisResult = table.Column<int>(nullable: false),
                    RequestAmountOfAnticipation = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    AnticipatedValue = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anticipation", x => x.Id);
                });

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
                    CardFinal = table.Column<string>(maxLength: 4, nullable: false),
                    AnticipationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Anticipation_AnticipationId",
                        column: x => x.AnticipationId,
                        principalTable: "Anticipation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Installment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InstallmentIdentifier = table.Column<int>(nullable: false),
                    TransactionInstallmentsId = table.Column<Guid>(nullable: false),
                    GrossValue = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NetValue = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    InstallmentNumber = table.Column<int>(nullable: false),
                    AnticipatedValue = table.Column<decimal>(nullable: true),
                    ReceiptDate = table.Column<string>(nullable: true),
                    TransferDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installment_Transaction_TransactionInstallmentsId",
                        column: x => x.TransactionInstallmentsId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installment_TransactionInstallmentsId",
                table: "Installment",
                column: "TransactionInstallmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AnticipationId",
                table: "Transaction",
                column: "AnticipationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installment");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Anticipation");
        }
    }
}
