using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjOrganizze.Api.Banco.Configuracao;
using ProjOrganizze.Api.Banco.Repositorios;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Services;
using ProjOrganizze.Api.Validators.Cartao;
using ProjOrganizze.Api.Validators.Conta;
using ProjOrganizze.Api.Validators.Transacao;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddDbContext<ContextoBase>(options =>
                options.UseSqlServer("name=ConnectionStrings:DefaultConnection")
            );
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<ICartaoRepository, CartaoRepository>();
builder.Services.AddScoped<IFaturaRepository, FaturaRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<ICartaoService, CartaoService>();

builder.Services.AddScoped<IValidator<CartaoAddDTO>, CartaoAddValidator>();
builder.Services.AddScoped<IValidator<CartaoUpdDTO>, CartaoUpdValidator>();
builder.Services.AddScoped<IValidator<ContaViewDTO>, ContaValidator>(); 
builder.Services.AddScoped<IValidator<ContaAddDTO>, ContaAddValidator>();
builder.Services.AddScoped<IValidator<TransacaoAddDTO>, TransacaoAddValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
