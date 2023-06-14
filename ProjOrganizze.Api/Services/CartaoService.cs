using Microsoft.EntityFrameworkCore;
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
            await NomeExiste(objeto.Nome);
            await _cartaoRepository.AddAsync(objeto);
            await _faturaRepository.AdicionarFaturas(objeto);
            // Adicionar await _unitOfWorkRepository.Commit();
        }

        public async Task<Cartao> AtualizarCartao(Cartao objeto)
        {
            await CartaoExiste(objeto.Id);
            var objetoDb = await _cartaoRepository.GetEntityByIdAsync(objeto.Id);
            if (objetoDb.Nome != objeto.Nome)
            {
                await NomeExiste(objeto.Nome);
            }

            objetoDb.Nome = objeto.Nome;
            objetoDb.Limite = objeto.Limite;
            objetoDb.DiaVencimento = objeto.DiaVencimento;
            await _cartaoRepository.UpdateAsync(objetoDb);
            return objetoDb;
        }

        public async Task DeletarCartao(int id)
        {
            await CartaoExiste(id);
            await _cartaoRepository.DeleteAsync(id);
        }

        public async Task<Cartao> ObterCartaoPorId(int id)
        {
            await CartaoExiste(id);
            var objetoDb = await _cartaoRepository.ObterCartaoPorId(id);
            return objetoDb;
        }

        public async Task<IEnumerable<Cartao>> ObterCartoes()
        {
            return await _cartaoRepository.ObterCartoes();
        }

        private async Task CartaoExiste(int id)
        {
            var objetoDb = await _cartaoRepository.GetEntityByIdAsync(id);
            if (objetoDb == null)
            {
                throw new ServiceException("Cartão não encontrado.");
            }
        }
        private async Task NomeExiste(string nome)
        {
            var cartoesDb = await _cartaoRepository.ObterCartoes();
            if (cartoesDb.Any(p => p.Nome == nome))
            {
                throw new ServiceException("Não é possível adicionar mais de um cartão com o mesmo nome.");
            }
        }
    }
}
