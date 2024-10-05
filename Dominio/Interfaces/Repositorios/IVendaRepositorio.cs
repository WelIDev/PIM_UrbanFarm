using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IVendaRepositorio
{
    void InserirVenda(Venda venda);
    Venda? ObterPorId(int id);
    List<Venda> ObterVendas();
    void AlterarVenda(Venda venda);
    void ExcluirVenda(Venda venda);
}
