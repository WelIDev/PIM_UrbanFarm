using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IVendaServico
{
    Task<bool> InserirVenda(VendaDto vendaDto);
    VendaDto ObterPorId(int id);
    List<Venda> ObterVendas();
    bool ExcluirVenda(int id);
}
