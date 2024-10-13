using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IAbastecimentoEstoqueRepositorio
{
    void InserirAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque);
    AbastecimentoEstoque? ObterPorId(int id);
    IList<AbastecimentoEstoque> ObterAbastecimentoEstoque();
    void AlterarAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque);
    void ExcluirAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque);
}
