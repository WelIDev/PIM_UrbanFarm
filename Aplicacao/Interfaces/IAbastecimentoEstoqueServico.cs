using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IAbastecimentoEstoqueServico
{
    Task<bool> InserirAbastecimentoEstoque(AbastecimentoEstoqueDto abastecimentoEstoqueDto);
    AbastecimentoEstoque ObterPorId(int id);
    IList<AbastecimentoEstoque> ObterAbastecimentoEstoque();
    bool AlterarAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque);
    bool ExcluirAbastecimentoEstoque(int id);
}
