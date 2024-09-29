using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IFornecedorServico
{
    bool Inserir(Fornecedor fornecedor);
    Fornecedor ObterPorId(int id);
    List<Fornecedor> ObterFornecedores();
    bool AlterarFornecedor(Fornecedor fornecedor);
    bool ExcluirFornecedor(int id);
}
