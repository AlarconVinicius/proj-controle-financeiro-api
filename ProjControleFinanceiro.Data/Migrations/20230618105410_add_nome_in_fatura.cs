using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleFinanceiro.Data.Migrations
{
    public partial class add_nome_in_fatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Faturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Faturas");
        }
    }
}
