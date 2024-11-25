using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Infraestrutura.Persistencia.Repositorios;

public class ProdutoRepositorio : IProdutoRepositorio
{
    private readonly AppDbContext _context;

    public ProdutoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();
    }

    public Produto? ObterPorId(int id)
    {
        return _context.Produtos.Find(id);
    }

    public IList<Produto> ObterProdutosPorId(IList<int> produtoIds)
    {
        return _context.Produtos.Where(p => produtoIds.Contains(p.Id)).ToList();
    }

    public List<Produto> ObterProdutos()
    {
        return _context.Produtos.ToList();
    }

    public void ExcluirProduto(Produto produto)
    {
        _context.Produtos.Remove(produto);
    }

    public void AlterarProduto(Produto produto)
    {
        _context.Produtos.Update(produto);
    }

    public async Task AtualizarEstoqueAsync(int id, int quantidade)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            produto.Estoque += quantidade;
            _context.Produtos.Update(produto);
        }
    }
}
