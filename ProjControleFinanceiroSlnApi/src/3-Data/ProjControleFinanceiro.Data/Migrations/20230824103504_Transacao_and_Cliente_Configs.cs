using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleFinanceiro.Data.Migrations;

/// <inheritdoc />
public partial class Transacao_and_Cliente_Configs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Valor",
            table: "Transacoes",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(double),
            oldType: "float");

        migrationBuilder.AlterColumn<bool>(
            name: "Repete",
            table: "Transacoes",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<int>(
            name: "QtdRepeticao",
            table: "Transacoes",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<bool>(
            name: "Pago",
            table: "Transacoes",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<string>(
            name: "Descricao",
            table: "Transacoes",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddCheckConstraint(
            name: "CK_Categoria_NaoZero",
            table: "Transacoes",
            sql: "Categoria > 0");

        migrationBuilder.AddCheckConstraint(
            name: "CK_TipoTransacao_NaoZero",
            table: "Transacoes",
            sql: "TipoTransacao > 0");

        migrationBuilder.AddCheckConstraint(
            name: "CK_Valor_NaoNegativo",
            table: "Transacoes",
            sql: "Valor >= 0");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(
            name: "CK_Categoria_NaoZero",
            table: "Transacoes");

        migrationBuilder.DropCheckConstraint(
            name: "CK_TipoTransacao_NaoZero",
            table: "Transacoes");

        migrationBuilder.DropCheckConstraint(
            name: "CK_Valor_NaoNegativo",
            table: "Transacoes");

        migrationBuilder.AlterColumn<double>(
            name: "Valor",
            table: "Transacoes",
            type: "float",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)",
            oldDefaultValue: 0m);

        migrationBuilder.AlterColumn<bool>(
            name: "Repete",
            table: "Transacoes",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<int>(
            name: "QtdRepeticao",
            table: "Transacoes",
            type: "int",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int",
            oldDefaultValue: 0);

        migrationBuilder.AlterColumn<bool>(
            name: "Pago",
            table: "Transacoes",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<string>(
            name: "Descricao",
            table: "Transacoes",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200);
    }
}
