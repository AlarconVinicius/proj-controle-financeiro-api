using ProjOrganizze.Api.Banco.Repositorios;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Exceptions;
using System;

namespace ProjOrganizze.Api.Services
{
    public class CartaoService : ICartaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IFaturaRepository _faturaRepository;
        public CartaoService(ICartaoRepository cartaoRepository, IContaRepository contaRepository, IFaturaRepository faturaRepository)
        {
            _cartaoRepository = cartaoRepository;
            _contaRepository = contaRepository;
            _faturaRepository = faturaRepository;
        }

        public async Task AdicionarCartao(Cartao objeto)
        {
            var contaExiste = await _contaRepository.GetEntityByIdAsync(objeto.ContaId);
            var cartoesDb = await _cartaoRepository.ObterCartoes();
            if (contaExiste == null)
            {
                throw new ServiceException("Conta inválida");
            }
            if(cartoesDb.Any(p => p.Nome == objeto.Nome))
            {
                throw new ServiceException("Não é possível adicionar mais de um cartão com o mesmo nome.");
            }
            await _cartaoRepository.AddAsync(objeto);
            await _faturaRepository.AdicionarFaturas(objeto);
            // Adicionar await _unitOfWorkRepository.Commit();
        }

        public async Task<Cartao> AtualizarCartao(Cartao objeto)
        {
            var objetoDb = await _cartaoRepository.GetEntityByIdAsync(objeto.Id);
            var cartoesDb = await _cartaoRepository.ObterCartoes();
            if (objetoDb == null)
            {
                throw new ServiceException("Cartão não encontrado.");
            }
            if(objetoDb.Nome != objeto.Nome)
            {
                if (cartoesDb.Any(p => p.Nome == objeto.Nome))
                {
                    throw new ServiceException("Não é possível adicionar mais de um cartão com o mesmo nome.");
                }
            }
            
            objetoDb.Nome = objeto.Nome;
            objetoDb.Limite = objeto.Limite;
            objetoDb.DiaVencimento = objeto.DiaVencimento;
            await _cartaoRepository.UpdateAsync(objetoDb);
            return objetoDb;
        }

        public async Task DeletarCartao(int id)
        {
            var objetoDb = await _cartaoRepository.GetEntityByIdAsync(id);
            if (objetoDb == null)
            {
                throw new ServiceException("Cartão não encontrado.");
            }
            await _cartaoRepository.DeleteAsync(id);
        }

        public async Task<Cartao> ObterCartaoPorId(int id)
        {
            var objetoDb = await _cartaoRepository.ObterCartaoPorId(id);
            if (objetoDb == null)
            {
                throw new ServiceException("Cartão não encontrado.");
            }
            return objetoDb;
        }

        public async Task<IEnumerable<Cartao>> ObterCartoes()
        {
            return await _cartaoRepository.ObterCartoes();
        }
    }
}
