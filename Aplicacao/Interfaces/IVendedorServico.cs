using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IVendedorServico
{
    bool InserirVendedor(Vendedor vendedor);
    Vendedor ObterPorId(int id);
    List<Vendedor?> ObterVendedores();
    bool AlterarVendedor(Vendedor vendedor);
    bool ExcluirVendedor(int id);
}
