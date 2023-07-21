using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;
using ProjControleFinanceiro.Domain.Extensions;
using System.Globalization;

namespace ProjControleFinanceiro.Domain.Validators.Transacao.Relatorio
{
    public class RelatorioAddValidator : AbstractValidator<RelatorioPDF>
    {
        public RelatorioAddValidator()
        {


            When(dto => dto.CicloPDf.Equals(CicloPDf.Periodo), () =>
            {
                RuleFor(dto => dto.Ate)
                .NotEmpty().WithMessage("Data de inicio precisa ser informada")
                .Must(validaFormatoData).WithMessage("A data precisa está no formato dd/mm/yyyy");


                RuleFor(dto => dto.De)
                    .NotEmpty().WithMessage("Data de fim precisa ser informada")
                    .Must(validaFormatoData).WithMessage("A data precisa está no formato dd/mm/yyyy")
                    .Must((dto, inicio) => ValidarDataInicio(inicio, dto.Ate)).WithMessage("Data de inicio precisa ser menor do que Data Fim");


                RuleFor(dto => dto.Ano)
                    .Empty().WithMessage("Para o filtro de Período: o ano não pode ser utilizado");

                RuleFor(dto => dto.Mes)
                    .Empty().WithMessage("Para o filtro de Período: o mês não pode ser utilizado");

            });


            When(dto => dto.CicloPDf.Equals(CicloPDf.Mensal), () =>
            {
                RuleFor(dto => dto.Mes)
                .InclusiveBetween(1, 12).WithMessage("Mes precisa ser entre 1 ou 12")
                .NotEmpty().WithMessage("O mês precisa ser informada");


                RuleFor(dto => dto.Ano)
                    .GreaterThanOrEqualTo(1900)
                    .NotEmpty().WithMessage("Ano precisa ser informado")
                    .WithMessage("O ano deve ser maior ou igual a 1900.");


                RuleFor(dto => dto.De)
                    .Empty().WithMessage("Para o filtro de Mensal: o período não pode ser utilizado");

                RuleFor(dto => dto.Ate)
                    .Empty().WithMessage("Para o filtro de Mensal: o período não pode ser utilizado");

            });

            When(dto => dto.CicloPDf.Equals(CicloPDf.Anual), () =>
            {

                RuleFor(dto => dto.Ano)
                    .NotEmpty().WithMessage("Data de fim precisa ser informada")
                    .GreaterThanOrEqualTo(1900)
                    .WithMessage("O ano deve ser maior ou igual a 1900.");


                RuleFor(dto => dto.De)
                    .Empty().WithMessage("O período não pode ser utilizado no relatório anual");

                RuleFor(dto => dto.Ate)
                    .Empty().WithMessage("O período não pode ser utilizado no relatório anual");

                RuleFor(dto => dto.Mes)
                    .Empty().WithMessage("O mês não pode ser utilizado no relatório anual");

            });


        }

        private bool ValidarDataInicio(string? dataFim, string? dataInicio)
        {
            DateTime dtInicio = dataInicio.ToDateTime();
            DateTime dtFim = dataFim.ToDateTime();

            return dtInicio > dtFim;
        }

        private bool validaFormatoData(string data)
        {
            if (!DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return false;
            }
            return true;
        }

    }


}
