using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IAbastecimentoEstoqueServico
{
    bool InserirAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque);
    AbastecimentoEstoque ObterPorId(int id);
    IList<AbastecimentoEstoque> ObterAbastecimentoEstoque();
    bool AlterarAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque);
    bool ExcluirAbastecimentoEstoque(int id);
}
