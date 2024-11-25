using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IProdutoRepositorio
{
    void InserirProduto(Produto produto);
    Produto? ObterPorId(int id);
    IList<Produto> ObterProdutosPorId(IList<int> produtoIds);
    List<Produto> ObterProdutos();
    void ExcluirProduto(Produto produto);
    void AlterarProduto(Produto produto);
    Task AtualizarEstoqueAsync(int id, int quantidade);
}
