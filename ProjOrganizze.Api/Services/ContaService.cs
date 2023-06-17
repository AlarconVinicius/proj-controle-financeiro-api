using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Banco.Repositorios;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Exceptions;
using ProjOrganizze.Api.Extensions;
using System;

namespace ProjOrganizze.Api.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }


        public async Task AdicionarConta(Conta conta)
        {
            await NomeExiste(conta.Nome);
            await _contaRepository.AddAsync(conta);
        }

        public async Task<Conta> AtualizarConta(Conta conta)
        {
            await ContaExiste(conta.Id);
            var objetoDb = await _contaRepository.GetEntityByIdAsync(conta.Id);
            if (objetoDb.Nome != conta.Nome)
            {
                await NomeExiste(conta.Nome);
            }
            objetoDb.Nome = conta.Nome;
            objetoDb.TipoConta = conta.TipoConta;

            await _contaRepository.UpdateAsync(objetoDb);

            return objetoDb;
        }

        public async Task DeletarConta(int id)
        {
            await ContaExiste(id);
            await _contaRepository.DeleteAsync(id);
        }

        public async Task<Conta> ObterContaPorId(int id)
        {
            await ContaExiste(id);
            return  await _contaRepository.GetEntityByIdAsync(id);
        }

        public async Task<IEnumerable<Conta>> ObterContas()
        {
            IEnumerable<Conta> contas = await _contaRepository.ListAsync();
            return contas;
        }
        private async Task ContaExiste(int id)
        {
            var objetoDb = await _contaRepository.GetEntityByIdAsync(id);
            if (objetoDb == null)
            {
                throw new ServiceException("Conta não encontrada.");
            }
        }
        private async Task NomeExiste(string nome)
        {
            var cartoesDb = await _contaRepository.ListAsync();
            if (cartoesDb.Any(p => p.Nome == nome))
            {
                throw new ServiceException("Não é possível adicionar mais de uma conta com o mesmo nome.");
            }
        }
    }
}