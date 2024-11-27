using Aplicacao.DTOs;
using Dominio.Dtos;
using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IProdutoServico
{
    bool InserirProduto(Produto produto);
    Produto ObterPorId(int id);
    List<Produto> ObterProdutos();
    bool ExcluirProduto(int id);
    bool AlterarProduto(Produto produto);
    Task<List<ProdutosMaisVendidosDto>> ObterProdutosMaisVendidosAsync();
    Task<List<ProdutoVendasDto>> ObterVendasPorProdutoAsync();
    Task<List<ProdutoEstoqueDto>> ObterNiveisEstoqueAsync();
    Task<List<ProdutoDto>> ObterUltimosProdutosAsync();
    Task<List<ProdutoVendaCustoDto>> ObterMargemLucroProdutosAsync();
    Task<ResumoFinanceiroDto> ObterResumoFinanceiroAsync();
    Task<List<TransacaoDto>> ObterDetalhesEntradasAsync();
    Task<List<MovimentacaoMonetariaDto>> ObterMovimentacoesMonetariasAsync();
}
