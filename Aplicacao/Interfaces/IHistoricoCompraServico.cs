using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IHistoricoCompraServico
{
    bool InserirHistoricoCompra(HistoricoCompra historicoCompra);
    HistoricoCompra ObterPorId(int id);
    List<HistoricoCompra> ObterHistoricoCompras();
    bool AlterarHistoricoCompra(HistoricoCompra historicoCompra);
    bool ExcluirHistoricoCompra(int id);
}
