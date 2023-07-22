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
using ProjControleFinanceiro.Entities.Entidades;
using System.Globalization;

namespace ProjControleFinanceiro.Domain.Services
{
    public class TransacaoService : MainService, ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IValidator<TransacaoAddDTO> _addValidator;
        private readonly IValidator<TransacaoUpdDTO> _updValidator;
        private readonly IValidator<RelatorioPDF> _addValidatorPdf;
        private string ciclo = "";

        public TransacaoService(ITransacaoRepository transacaoRepository, IValidator<TransacaoAddDTO> addValidator, IValidator<TransacaoUpdDTO> updValidator, IValidator<RelatorioPDF> addValidatorPdf)
        {
            _transacaoRepository = transacaoRepository;
            _addValidator = addValidator;
            _updValidator = updValidator;
            _addValidatorPdf = addValidatorPdf;
        }

        public async Task<TransacaoViewDTO> AdicionarTransacao(TransacaoAddDTO objeto)
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null;
            }
            Transacao objetoMapeado = objeto.ToAddDTO();
            await _transacaoRepository.AddAsync(objetoMapeado);
            if (objetoMapeado.QtdRepeticao > 0)
            {
                for (int i = 0; i <= objetoMapeado.QtdRepeticao - 1; i++)
                {
                    DateTime dataFutura = objetoMapeado.Data.AddMonths(i+1);
                    Transacao transacaoFutura = new Transacao(objetoMapeado.Descricao, objetoMapeado.Valor, dataFutura, objetoMapeado.TipoTransacao, objetoMapeado.Categoria);
                    await _transacaoRepository.AddAsync(transacaoFutura);
                }
            }
            return objetoMapeado.ToGetDTO();
        }

        public async Task<TransacaoViewDTO> ObterTransacaoPorId(int id)
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);
            if (transacao == null)
            {
                AdicionarErroProcessamento("Transação não encontrada.");
                return null;
            }
            TransacaoViewDTO objetoMapeadoView = transacao.ToGetDTO();
            return objetoMapeadoView;
        }

        public async Task<IEnumerable<TransacaoViewDTO>> ObterTransacoes()
        {
            IEnumerable<Transacao> objetosDb = await _transacaoRepository.ObterTransacoes();
            return objetosDb.Select(x => x.ToGetDTO());
        }

        public async Task<bool> AtualizarTransacao(TransacaoUpdDTO objeto)
        {
            var validationResult = await _updValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return false;
            }
            var objetoDb = await _transacaoRepository.ObterTransacaoPorId(objeto.Id);

            if (objetoDb == null)
            {
                AdicionarErroProcessamento("Transação não encontrada.");
                return false;
            }
            objetoDb.Descricao = objeto.Descricao;
            objetoDb.Valor = objeto.Valor;
            objetoDb.Data = objeto.Data.ToDateTime();
            objetoDb.Valor = objeto.Valor;
            objetoDb.TipoTransacao = objeto.TipoTransacao;
            objetoDb.Categoria = objeto.Categoria;
            await _transacaoRepository.UpdateAsync(objetoDb);
            return true;
        }

        public async Task<bool> AtualizarStatusPagamento(int id, bool pago)
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);

            if (transacao == null)
            {
                AdicionarErroProcessamento("Transação não encontrada.");
                return false;
            }

            transacao.Pago = pago;
            await _transacaoRepository.UpdateAsync(transacao);
            return true;
        }

        public async Task<bool> DeletarTransacao(int id)
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);

            if (transacao == null)
            {
                AdicionarErroProcessamento("Transação não encontrada.");
                return false;
            }
            await _transacaoRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Byte[]> GerarRelatorio(RelatorioPDF query)
        {

            var validationResult = await _addValidatorPdf.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null;
            }

            IEnumerable<TransacaoViewDTO> listagemTransacao = ObterTransacaoPorTipo(query).Select(t => t.ToGetDTORelatorio()).ToList();

            
            if (!listagemTransacao.Any()) {
                AdicionarErroProcessamento("Transação vazia para o período");
                return null;
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
           
            for (int i = 0; i < listaTransacao.Count() ; i++)
            {
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].Descricao)));
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].Valor.ToString())));
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].Data.ToString())));
                tabela.AddCell(new Cell().SetPadding(10).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(listaTransacao[i].PagoRelaorio)));
            }

            document.Add(tabela);

            return true;
        }                       


        public IEnumerable<Transacao> ObterTransacaoPorTipo(RelatorioPDF query)
        {
            if (!(string.IsNullOrEmpty(query.De) && string.IsNullOrEmpty(query.Ate)))
            {

                ciclo = $"Transações do período de {query.De} ate {query.Ate}";
                return _transacaoRepository.ObterTransacaoPeriodo(query.De, query.Ate).ToList();
            }
            else if(query.Mes.HasValue && query.Ano.HasValue)
            {

                string mes = new DateTimeFormatInfo().GetMonthName(query.Mes.Value).ToString();
                ciclo = $"Transações do mês {mes}/ {query.Ano}";
                return _transacaoRepository.ObterTransacaoPorMes(query.Mes, query.Ano).ToList();
            }
            else if (query.Ano.HasValue)
            {

                ciclo = $"Transações do ano: {query.Ano}";
                return _transacaoRepository.ObterTransacaoPorAno(query.Ano).ToList();
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