using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
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


        public async Task Adicionar(Conta conta)
        {
            await _contaRepository.AddAsync(conta);
        }

        public async Task<Conta> AtualizarConta(Conta conta)
        {
            var objetoDb = await _contaRepository.GetEntityByIdAsync(conta.Id);

            objetoDb.Nome = conta.Nome;
            objetoDb.TipoConta = conta.TipoConta;

            await _contaRepository.UpdateAsync(objetoDb);

            return objetoDb;
        }

        public async Task DeletarConta(int id)
        {
            await _contaRepository.DeleteAsync(id);
        }

        public async Task<Conta> ContaId(int id)
        {
            return  await _contaRepository.GetEntityByIdAsync(id);
        }

        public async Task<IEnumerable<Conta>> ListarContas()
        {
            IEnumerable<Conta> contas = await _contaRepository.ListAsync();
            return contas;
        }

    }
}