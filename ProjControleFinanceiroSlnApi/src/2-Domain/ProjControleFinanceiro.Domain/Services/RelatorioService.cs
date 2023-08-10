using FluentValidation;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Domain.Services.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using System.Globalization;

namespace ProjControleFinanceiro.Domain.Services
{
    public class RelatorioService : MainService, IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly IValidator<RelatorioPDF> _addValidatorPdf;
        private string ciclo = "";

        public RelatorioService(IRelatorioRepository relatorioRepository, IValidator<RelatorioPDF> addValidatorPdf)
        {
            _relatorioRepository = relatorioRepository;
            _addValidatorPdf = addValidatorPdf;
        }

        public async Task<Byte[]> GerarRelatorio(RelatorioPDF query)
        {

            var validationResult = await _addValidatorPdf.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null!;
            }

            IEnumerable<TransacaoViewDTO> listagemTransacao = ObterTransacaoPorTipo(query).Select(t => t.ToGetDTORelatorio()).ToList();


            if (!listagemTransacao.Any())
            {
                AdicionarErroProcessamento("Transação vazia para o período");
                return null!;
            }

            var ms = await CriarPdf(listagemTransacao);
            return ms.ToArray();


        }

        private async Task<MemoryStream> CriarPdf(IEnumerable<TransacaoViewDTO> listaTransacao)
        {
            MemoryStream ms = new MemoryStream();

            await Task.Run(() =>
            {

                using (PdfWriter wpf = new PdfWriter(ms))
                {
                    var pdfDocument = new PdfDocument(wpf);
                    var document = new Document(pdfDocument, PageSize.A4);

                    Paragraph heading = new Paragraph("Controle Financeiro")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetFontSize(18);
                    document.Add(heading);

                    float[] columnsWidth = { 30, 10, 10, 10 };
                    Table tabela = new Table(UnitValue.CreatePercentArray(columnsWidth)).UseAllAvailableWidth();
                    var fonte = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    tabela.AddHeaderCell(new Cell(1, 4).Add(new Paragraph(ciclo))
                        .SetFont(fonte)
                        .SetPadding(25)
                        .SetFontSize(15)
                        .SetFontColor(ColorConstants.BLACK)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetTextAlignment(TextAlignment.CENTER));

                    tabela.AddHeaderCell(new Cell()
                         .SetPadding(10)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetBackgroundColor(ColorConstants.WHITE)
                        .Add(new Paragraph("Descrição")));
                    tabela.AddHeaderCell(new Cell()
                        .SetPadding(10)
                       .SetTextAlignment(TextAlignment.LEFT)
                       .SetBackgroundColor(ColorConstants.WHITE)
                       .Add(new Paragraph("Valor")));
                    tabela.AddHeaderCell(new Cell()
                       .SetPadding(10)
                      .SetTextAlignment(TextAlignment.LEFT)
                      .SetBackgroundColor(ColorConstants.WHITE)
                      .Add(new Paragraph("Data")));
                    tabela.AddHeaderCell(new Cell()
                      .SetPadding(10)
                       .SetTextAlignment(TextAlignment.LEFT)
                      .SetBackgroundColor(ColorConstants.WHITE)
                      .Add(new Paragraph("Pago")));

                    PopularColunas(tabela, document, (IList<TransacaoViewDTO>)listaTransacao);

                    document.Close();
                    pdfDocument.Close();
                }
            });
            return ms;
        }

        private bool PopularColunas(Table tabela, Document document, IList<TransacaoViewDTO> listaTransacao)
        {

            for (int i = 0; i < listaTransacao.Count(); i++)
            {
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].Descricao)));
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].Valor.ToString())));
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].Data.ToString())));
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].PagoRelatorio)));
            }

            document.Add(tabela);

            return true;
        }


        public IEnumerable<Transacao> ObterTransacaoPorTipo(RelatorioPDF query)
        {
            if (!(string.IsNullOrEmpty(query.De) && string.IsNullOrEmpty(query.Ate)))
            {

                ciclo = $"Transações do período de {query.De} ate {query.Ate}";
                return _relatorioRepository.ObterTransacaoPeriodo(query.De, query.Ate).ToList();
            }
            else if (query.Mes.HasValue && query.Ano.HasValue)
            {

                string mes = new DateTimeFormatInfo().GetMonthName(query.Mes.Value).ToString();
                ciclo = $"Transações do mês {mes}/ {query.Ano}";
                return _relatorioRepository.ObterTransacaoPorMes(query.Mes, query.Ano).ToList();
            }
            else if (query.Ano.HasValue)
            {

                ciclo = $"Transações do ano: {query.Ano}";
                return _relatorioRepository.ObterTransacaoPorAno(query.Ano).ToList();
            }
            else
            {
                AdicionarErroProcessamento("Filtro incorreto");
            }


            return Enumerable.Empty<Transacao>();
            //Melhorar o erro !
        }
    }
}
