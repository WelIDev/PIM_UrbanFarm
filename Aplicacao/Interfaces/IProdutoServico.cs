using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IProdutoServico
{
    bool InserirProduto(Produto produto);
    Produto ObterPorId(int id);
    List<Produto> ObterProdutos();
    bool ExcluirProduto(int id);
    bool AlterarProduto(Produto produto);
}
