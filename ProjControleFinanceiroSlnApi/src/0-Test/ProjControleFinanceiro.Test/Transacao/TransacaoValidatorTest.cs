using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Validators.Transacao;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Test.Transacao;

public class TransacaoValidatorTest
{
    private readonly TransacaoAddValidator _addValidator = new TransacaoAddValidator();
    private readonly TransacaoUpdValidator _updValidator = new TransacaoUpdValidator();

    #region Testes Adição
    [Theory]
    [InlineData("", "13/06/2023", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Descricao é obrigatório.")]
    [InlineData("Descrição", "", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "A data é obrigatória.")]
    [InlineData("Descrição", "DataTeste", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Data deve seguir o padrão dd/MM/yyyy.")]
    [InlineData("Descrição", "13/06/2023", 0.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Valor é obrigatório.")]
    [InlineData("Descrição", "13/06/2023", 0.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Valor deve ser maior que 0.")]
    [InlineData("Descrição", "13/06/2023", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, true, 0, "O campo QtdRepeticao é obrigatório.")]
    [InlineData("Descrição", "13/06/2023", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, true, 0, "O campo QtdRepeticao deve ser maior que 0.")]
    public void TransacaoAddValidator_Should_ReturnFailureResult_WhenInvalidObject(string description, string date, double value, TipoTransacao type, Categoria category, bool pay, bool repete, int qtdRep, string expectedErrorMessage)
    {
        // Arrange
        var objeto = new TransacaoAddDto
        {
            Descricao = description,
            Valor = value,
            TipoTransacao = type,
            Categoria = category,
            Data = date,
            Pago = pay,
            Repete = repete,
            QtdRepeticao = qtdRep
        };

        // Act
        var result = _addValidator.Validate(objeto);

        // Assert
        Assert.False(result.IsValid);
        var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        Assert.Contains(expectedErrorMessage, errors);
    }
    [Fact]
    public void TransacaoAddValidator_Should_ReturnSuccessResult_WhenValidObject()
    {
        // Arrange
        var objeto = new TransacaoAddDto
        {
            Descricao = "Descrição",
            Valor = 100.0,
            TipoTransacao = TipoTransacao.Despesa,
            Categoria = Categoria.Outros,
            Data = "13/06/2023",
            Pago = true,
            Repete = false,
            QtdRepeticao = 0
        };

        // Act
        var result = _addValidator.Validate(objeto);

        // Assert
        Assert.True(result.IsValid);
    }
    #endregion

    #region Testes Atualizar
    [Theory]
    [InlineData("", "13/06/2023", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Descricao é obrigatório.")]
    [InlineData("Descrição", "", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "A data é obrigatória.")]
    [InlineData("Descrição", "DataTeste", 100.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Data deve seguir o padrão dd/MM/yyyy.")]
    [InlineData("Descrição", "13/06/2023", 0.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Valor é obrigatório.")]
    [InlineData("Descrição", "13/06/2023", 0.0, TipoTransacao.Despesa, Categoria.Outros, true, false, 0, "O campo Valor deve ser maior que 0.")]
    public void TransacaoUpdValidator_Should_ReturnFailureResult_WhenInvalidObject(string description, string date, double value, TipoTransacao type, Categoria category, bool pay, bool repete, int qtdRep, string expectedErrorMessage)
    {
        // Arrange
        var objeto = new TransacaoUpdDto
        {
            Descricao = description,
            Valor = value,
            TipoTransacao = type,
            Categoria = category,
            Data = date
        };

        // Act
        var result = _updValidator.Validate(objeto);

        // Assert
        Assert.False(result.IsValid);
        var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        Assert.Contains(expectedErrorMessage, errors);
    }
    [Fact]
    public void TransacaoUpdValidator_Should_ReturnSuccessResult_WhenValidObject()
    {
        // Arrange
        var objeto = new TransacaoUpdDto
        {
            Descricao = "Descrição",
            Valor = 100.0,
            TipoTransacao = TipoTransacao.Despesa,
            Categoria = Categoria.Outros,
            Data = "13/06/2023"
        };

        // Act
        var result = _updValidator.Validate(objeto);

        // Assert
        Assert.True(result.IsValid);
    }
    #endregion
}
