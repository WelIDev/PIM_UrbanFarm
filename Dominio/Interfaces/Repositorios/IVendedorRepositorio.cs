using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IVendedorRepositorio
{
    void InserirVendedor(Vendedor? vendedor);
    Vendedor? ObterPorId(int id);
    List<Vendedor?> ObterVendedores();
    void AlterarVendedor(Vendedor vendedor);
    void ExcluirVendedor(Vendedor vendedor);
}
