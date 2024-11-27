using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class ProdutoServico : IProdutoServico
{
    private readonly IProdutoRepositorio _produtoRepositorio;

    public ProdutoServico(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }

    public bool InserirProduto(Produto produto)
    {
        ArgumentNullException.ThrowIfNull(produto);

        if (string.IsNullOrWhiteSpace(produto.Nome))
        {
            throw new ArgumentException("Nome do produto é obrigatório.");
        }

        try
        {
            _produtoRepositorio.InserirProduto(produto);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public Produto ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var produto = _produtoRepositorio.ObterPorId(id);
        if (produto == null)
        {
            throw new KeyNotFoundException("Produto não encontrado.");
        }

        return produto;
    }

    public List<Produto> ObterProdutos()
    {
        var produtos = _produtoRepositorio.ObterProdutos();
        if (produtos == null || produtos.Count == 0)
        {
            throw new KeyNotFoundException("Nenhum fornecedor encontrado.");
        }

        return produtos;
    }

    public bool ExcluirProduto(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var produto = _produtoRepositorio.ObterPorId(id);
        try
        {
            if (produto != null)
            {
                _produtoRepositorio.ExcluirProduto(produto);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar excluir: " + e.Message);
            return false;
        }
    }

    public bool AlterarProduto(Produto produto)
    {
        ArgumentNullException.ThrowIfNull(produto);

        if (string.IsNullOrWhiteSpace(produto.Nome))
        {
            throw new ArgumentException("Nome do produto é obrigatório.");
        }

        try
        {
            _produtoRepositorio.AlterarProduto(produto);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar : " + e.Message);
            return false;
        }
    }

    public async Task<List<ProdutosMaisVendidosDto>> ObterProdutosMaisVendidosAsync()
    {
        return await _produtoRepositorio.ObterProdutosMaisVendidosAsync();
    }

    public async Task<List<ProdutoVendasDto>> ObterVendasPorProdutoAsync()
    {
        return await _produtoRepositorio.ObterVendasPorProdutoAsync();
    }

    public async Task<List<ProdutoEstoqueDto>> ObterNiveisEstoqueAsync()
    {
        return await _produtoRepositorio.ObterNiveisEstoqueAsync();
    }

    public async Task<List<ProdutoDto>> ObterUltimosProdutosAsync()
    {
        return await _produtoRepositorio.ObterUltimosProdutosAsync();
    }

    public async Task<List<ProdutoVendaCustoDto>> ObterMargemLucroProdutosAsync()
    {
        var produtos = await _produtoRepositorio.ObterVendasCustosProdutosAsync();
        foreach (var produto in produtos)
        {
            produto.MargemLucro = produto.TotalVendas - produto.TotalCusto;
        }

        return produtos.OrderByDescending(p => p.MargemLucro).ToList();
    }

    public async Task<ResumoFinanceiroDto> ObterResumoFinanceiroAsync()
    {
        return await _produtoRepositorio.ObterResumoFinanceiroAsync();
    }

    public async Task<List<TransacaoDto>> ObterDetalhesEntradasAsync()
    {
        return await _produtoRepositorio.ObterDetalhesEntradasAsync();
    }

    public async Task<List<MovimentacaoMonetariaDto>> ObterMovimentacoesMonetariasAsync()
    {
        return await _produtoRepositorio.ObterMovimentacoesMonetariasAsync();
    }
}
