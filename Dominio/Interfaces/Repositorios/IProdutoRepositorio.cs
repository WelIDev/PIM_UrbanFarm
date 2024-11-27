using Aplicacao.DTOs;
using Dominio.Dtos;
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
    Task<List<ProdutosMaisVendidosDto>> ObterProdutosMaisVendidosAsync();
    Task<List<ProdutoVendasDto>> ObterVendasPorProdutoAsync();
    Task<List<ProdutoEstoqueDto>> ObterNiveisEstoqueAsync();
    Task<List<ProdutoDto>> ObterUltimosProdutosAsync();
    Task<List<ProdutoVendaCustoDto>> ObterVendasCustosProdutosAsync();
    Task<ResumoFinanceiroDto> ObterResumoFinanceiroAsync();
    Task<List<TransacaoDto>> ObterDetalhesEntradasAsync();
    Task<List<MovimentacaoMonetariaDto>> ObterMovimentacoesMonetariasAsync();
}
