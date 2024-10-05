using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class VendaServico : IVendaServico
{
    private readonly IVendaRepositorio _vendaRepositorio;
    private readonly IProdutoRepositorio _produtoRepositorio;

    public VendaServico(IVendaRepositorio vendaRepositorio, IProdutoRepositorio produtoRepositorio)
    {
        _vendaRepositorio = vendaRepositorio;
        _produtoRepositorio = produtoRepositorio;
    }

    public bool InserirVenda(VendaDto vendaDto)
    {
        ArgumentNullException.ThrowIfNull(vendaDto);
        try
        {
            var produtos = _produtoRepositorio.ObterProdutosPorId(vendaDto.ProdutoIds);
            var venda = new Venda
            {
                Valor = vendaDto.Valor,
                VendedorId = vendaDto.VendedorId,
                ClienteId = vendaDto.ClienteId,
                FormaDePagamento = vendaDto.FormaDePagamento,
                Produtos = produtos
            };
            
            _vendaRepositorio.InserirVenda(venda);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public Venda ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var venda = _vendaRepositorio.ObterPorId(id);
        if (venda == null)
        {
            throw new KeyNotFoundException("Venda não encontrada.");
        }

        return venda;
    }

    public List<Venda> ObterVendas()
    {
        var vendas = _vendaRepositorio.ObterVendas();
        if (vendas.Count == 0)
        {
            throw new KeyNotFoundException("Nenhuma venda encontrada.");
        }

        return vendas;
    }

    public bool AlterarVenda(int id, VendaDto vendaDto)
    {
        ArgumentNullException.ThrowIfNull(vendaDto);
        try
        {
            var venda = _vendaRepositorio.ObterPorId(id);
            if (venda == null)
            {
                throw new KeyNotFoundException("Venda não encontrada");
            }

            var produtos = _produtoRepositorio.ObterProdutosPorId(vendaDto.ProdutoIds);
            
            venda.Valor = vendaDto.Valor;
            venda.VendedorId = vendaDto.VendedorId;
            venda.ClienteId = vendaDto.ClienteId;
            venda.FormaDePagamento = vendaDto.FormaDePagamento;
            venda.Produtos = produtos;
            
            _vendaRepositorio.AlterarVenda(venda);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar: " + e.Message);
            return false;
        }
    }

    public bool ExcluirVenda(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var venda = _vendaRepositorio.ObterPorId(id);
        try
        {
            if (venda != null)
            {
                _vendaRepositorio.ExcluirVenda(venda);
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
