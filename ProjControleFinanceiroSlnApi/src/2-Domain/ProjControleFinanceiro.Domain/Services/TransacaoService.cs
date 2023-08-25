using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Domain.Services.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Services;

public class TransacaoService : MainService, ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IValidator<TransacaoAddDto> _addValidator;
    private readonly IValidator<TransacaoUpdDto> _updValidator;
    private readonly IUser _user;
    public TransacaoService(ITransacaoRepository transacaoRepository, IValidator<TransacaoAddDto> addValidator, IValidator<TransacaoUpdDto> updValidator, IUser user)
    {
        _transacaoRepository = transacaoRepository;
        _addValidator = addValidator;
        _updValidator = updValidator;
        _user = user;
    }
    public async Task<TransacaoViewDto> AdicionarTransacao(TransacaoAddDto objeto)
    {
        try
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null!;
            }
            objeto.ClienteId = _user.GetUserId();
            Transacao objetoMapeado = objeto.ToAddDTO();
            await _transacaoRepository.AddAsync(objetoMapeado);
            if (objetoMapeado.QtdRepeticao > 0)
            {
                for (int i = 0; i <= objetoMapeado.QtdRepeticao - 1; i++)
                {
                    DateTime dataFutura = objetoMapeado.Data.AddMonths(i + 1);
                    Transacao transacaoFutura = new Transacao(objetoMapeado.ClienteId,objetoMapeado.Descricao, objetoMapeado.Valor, dataFutura, objetoMapeado.TipoTransacao, objetoMapeado.Categoria);
                    await _transacaoRepository.AddAsync(transacaoFutura);
                }
            }
            return objetoMapeado.ToGetDTO();
        }
        catch (ArgumentNullException ex)
        {
            List<string> errors = new List<string>();
            errors.Add("Erro ao adicionar a transação!");
            errors.Add("Nenhum campo pode ser nulo.");
            errors.Add(ex.Message);
            AdicionarErroProcessamento(errors);
            return new TransacaoViewDto();
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao adicionar a transação: {ex.Message}");
            return new TransacaoViewDto();
        }
    }

    public async Task<TransacaoViewDto> ObterTransacaoPorId(Guid id)
    {
        try
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);
            if (transacao is null)
            {
                return null!;
            }
            TransacaoViewDto objetoMapeadoView = transacao.ToGetDTO();
            return objetoMapeadoView;
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Falha ao buscar a transação: {ex.Message}");
            return null!;
        }
    }

    public async Task<TransacaoViewListDto> ObterTransacoes()
    {
        try
        {
            TransacaoViewListDto transacoesList = new TransacaoViewListDto();
            IEnumerable<Transacao> objetosDb = await _transacaoRepository.ObterTransacoes();
            if (!objetosDb.Any()) return null!;
            foreach (var transacao in objetosDb)
            {
                if (transacao.TipoTransacao == TipoTransacaoEnum.Receita)
                {
                    transacoesList.Entrada += transacao.Valor;
                }
                else
                {
                    transacoesList.Saida += transacao.Valor;
                }
            }
            transacoesList.Saldo = transacoesList.Entrada - transacoesList.Saida;
            transacoesList.Transacoes = objetosDb.Select(x => x.ToGetDTO());
            return transacoesList;
        }

        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Falha ao buscar as transações: {ex.Message}");
            return null!;
        }
    }
    public async Task<TransacaoViewListDto> ObterTransacoesMesAno(int mes, int ano)
    {
        try
        {
            TransacaoViewListDto transacoesList = new TransacaoViewListDto();
            if (mes < 1 || mes > 12)
            {
                AdicionarErroProcessamento("Mês inválido, o valor deve estar entre 1 e 12.");
                return null!;
            }
            IEnumerable<Transacao> objetosDb = await _transacaoRepository.ObterTransacoesMesAno(mes, ano);
            if (!objetosDb.Any()) return null!;
            foreach (var transacao in objetosDb)
            {
                if (transacao.TipoTransacao == TipoTransacaoEnum.Receita)
                {
                    transacoesList.Entrada += transacao.Valor;
                }
                else
                {
                    transacoesList.Saida += transacao.Valor;
                }
            }
            transacoesList.Saldo = transacoesList.Entrada - transacoesList.Saida;
            transacoesList.Transacoes = objetosDb.Select(x => x.ToGetDTO());
            return transacoesList;
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Falha ao buscar as transações: {ex.Message}");
            return null!;
        }
    }
    public async Task<bool> AtualizarTransacao(TransacaoUpdDto objeto)
    {
        try
        {
            var validationResult = await _updValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return false;
            }
            var objetoDb = await _transacaoRepository.ObterTransacaoPorId(objeto.Id);

            if (objetoDb is null)
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
        catch (ArgumentNullException ex)
        {
            List<string> errors = new List<string>();
            errors.Add("Erro ao atualizar a transação!");
            errors.Add("Nenhum campo pode ser nulo.");
            errors.Add(ex.Message);
            AdicionarErroProcessamento(errors);
            return false;
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao atualizar a transação: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> AtualizarStatusPagamento(Guid id, bool pago)
    {
        try
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);

            if (transacao is null)
            {
                AdicionarErroProcessamento("Transação não encontrada.");
                return false;
            }

            transacao.Pago = pago;
            await _transacaoRepository.UpdateAsync(transacao);
            return true;
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Falha ao atualizar o status de pagamento da transação: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeletarTransacao(Guid id)
    {
        try
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);

            if (transacao is null)
            {
                AdicionarErroProcessamento("Transação não encontrada.");
                return false;
            }
            await _transacaoRepository.DeleteAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Falha ao deletar a transação: {ex.Message}");
            return false;
        }
    }

}