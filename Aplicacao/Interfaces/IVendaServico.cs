using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IVendaServico
{
    bool InserirVenda(VendaDto vendaDto);
    Venda ObterPorId(int id);
    List<Venda> ObterVendas();
    bool AlterarVenda(int id, VendaDto vendaDto);
    bool ExcluirVenda(int id);
}
