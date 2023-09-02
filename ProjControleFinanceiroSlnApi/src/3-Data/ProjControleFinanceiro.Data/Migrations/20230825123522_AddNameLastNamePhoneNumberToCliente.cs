using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleFinanceiro.Data.Migrations;

/// <inheritdoc />
public partial class AddNameLastNamePhoneNumberToCliente : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "LastName",
            table: "Clientes",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Clientes",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "PhoneNumber",
            table: "Clientes",
            type: "nvarchar(30)",
            maxLength: 30,
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "LastName",
            table: "Clientes");

        migrationBuilder.DropColumn(
            name: "Name",
            table: "Clientes");

        migrationBuilder.DropColumn(
            name: "PhoneNumber",
            table: "Clientes");
    }
}
