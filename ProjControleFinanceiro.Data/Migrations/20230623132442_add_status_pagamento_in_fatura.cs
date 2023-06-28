using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleFinanceiro.Data.Migrations
{
    public partial class add_status_pagamento_in_fatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusPagamento",
                table: "Faturas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusPagamento",
                table: "Faturas");
        }
    }
}
