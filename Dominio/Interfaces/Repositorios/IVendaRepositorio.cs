using Dominio.Dtos;
using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IVendaRepositorio
{
    void InserirVenda(Venda venda);
    Venda? ObterPorId(int id);
    List<Venda> ObterVendas();
    void ExcluirVenda(Venda venda);
    Task<List<VendaMensalDto>> ObterVendasMensaisAsync();
}
