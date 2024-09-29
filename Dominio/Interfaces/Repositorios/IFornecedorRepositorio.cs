using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IFornecedorRepositorio
{
    void InserirFornecedor(Fornecedor fornecedor);
    Fornecedor? ObterPorId(int id);
    List<Fornecedor> ObterFornecedores();
    void AtualizarFornecedor(Fornecedor fornecedor);
    void ExcluirFornecedor(Fornecedor fornecedor);
}
