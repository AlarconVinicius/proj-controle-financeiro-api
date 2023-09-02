using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleFinanceiro.Data.Migrations;

/// <inheritdoc />
public partial class RemovePhoneNumberFromCliente : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PhoneNumber",
            table: "Clientes");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "PhoneNumber",
            table: "Clientes",
            type: "nvarchar(30)",
            maxLength: 30,
            nullable: false,
            defaultValue: "");
    }
}
