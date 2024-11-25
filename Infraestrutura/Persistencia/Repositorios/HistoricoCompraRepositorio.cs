using System.Data;
using System.Data.SqlClient;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Persistencia.Repositorios;

public class HistoricoCompraRepositorio : IHistoricoCompraRepositorio
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;

    public HistoricoCompraRepositorio(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("Default");
    }

    public void InserirHistorico(HistoricoCompra? historicoCompra)
    {
        _context.HistoricoCompras.Add(historicoCompra);
        _context.SaveChanges();
    }

    public HistoricoCompra? ObterPorId(int id)
    {
        return _context.HistoricoCompras.Find(id);
    }

    public List<HistoricoCompra> ObterHistoricosCompras()
    {
        return _context.HistoricoCompras.ToList();
    }
    
    public async Task<HistoricoCompra> ObterHistoricoCompraPorClienteId(int clienteId)
    {
        HistoricoCompra historico = null;
        var vendas = new List<Venda>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("GetHistoricoComprasByClienteId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("clienteId", clienteId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (historico == null)
                        {
                            historico = new HistoricoCompra
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("HistoricoId")),
                                Cliente = new Cliente
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                    Nome = reader.GetString(reader.GetOrdinal("ClienteNome"))
                                },
                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"))
                            };
                        }
                        vendas.Add(new Venda
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("VendaId")),
                            Data = reader.GetDateTime(reader.GetOrdinal("VendaData")),
                            Valor = reader.GetDouble(reader.GetOrdinal("VendaValor"))
                        });
                    }
                }
            }
        }

        if (historico != null)
        {
            historico.Vendas = vendas;
        }

        return historico;
    }


    public void AlterarHistorico(HistoricoCompra historicoCompra)
    {
        _context.HistoricoCompras.Update(historicoCompra);
        _context.SaveChanges();
    }

    public void ExcluirHistorico(HistoricoCompra historicoCompra)
    {
        _context.HistoricoCompras.Remove(historicoCompra);
        _context.SaveChanges();
    }
}
