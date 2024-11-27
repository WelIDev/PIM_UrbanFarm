using Aplicacao.DTOs;
using Dominio.Dtos;
using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IVendedorServico
{
    bool InserirVendedor(VendedorInserirDto vendedor);
    Vendedor ObterPorId(int id);
    List<Vendedor?> ObterVendedores();
    Task<List<VendedorDto>> ObterVendedoresComVendasAsync(DateTime dataInicio, DateTime dataFim);
    bool AlterarVendedor(Vendedor vendedor);
    bool ExcluirVendedor(int id);
}
