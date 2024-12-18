﻿using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class VendedorServico : IVendedorServico
{
    private readonly IVendedorRepositorio _vendedorRepositorio;

    public VendedorServico(IVendedorRepositorio vendedorRepositorio)
    {
        _vendedorRepositorio = vendedorRepositorio;
    }

    public bool InserirVendedor(VendedorInserirDto vendedorInserirDto)
    {
        ArgumentNullException.ThrowIfNull(vendedorInserirDto);

        var vendedor = new Vendedor
        {
            Nome = vendedorInserirDto.Nome,
            Salario = vendedorInserirDto.Salario,
            CpfCnpj = vendedorInserirDto.CpfCnpj,
            Telefone = vendedorInserirDto.Telefone,
            DataContratacao = vendedorInserirDto.DataContratacao,
            Endereco = vendedorInserirDto.Endereco
        };
        try
        {
            _vendedorRepositorio.InserirVendedor(vendedor);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public Vendedor ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var vendedor = _vendedorRepositorio.ObterPorId(id);
        if (vendedor == null)
        {
            throw new KeyNotFoundException("Vendedor não encontrado.");
        }

        return vendedor;
    }
    
    public List<Vendedor?> ObterVendedores()
    {
        var vendedores = _vendedorRepositorio.ObterVendedores();
        if (vendedores  == null || vendedores.Count == 0)
        {
            throw new KeyNotFoundException("Nenhum vendedor encontrado");
        }

        return vendedores;
    }

    public async Task<List<VendedorDto>> ObterVendedoresComVendasAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _vendedorRepositorio.ObterVendedoresComVendasAsync(dataInicio, dataFim);
    }

    public bool AlterarVendedor(Vendedor vendedor)
    {
        ArgumentNullException.ThrowIfNull(vendedor);
        try
        {
            _vendedorRepositorio.AlterarVendedor(vendedor);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar: " + e.Message);
            return false;
        }
    }

    public bool ExcluirVendedor(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var vendedor = _vendedorRepositorio.ObterPorId(id);
        try
        {
            if (vendedor != null)
            {
                _vendedorRepositorio.ExcluirVendedor(vendedor);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar excluir: " + e.Message);
            return false;
        }
    }
}
