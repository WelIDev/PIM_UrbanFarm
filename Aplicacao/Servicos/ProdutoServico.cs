using Aplicacao.Interfaces;
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
}
