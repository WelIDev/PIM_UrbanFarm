using System.Data;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Persistencia.Repositorios;

public class VendaRepositorio : IVendaRepositorio
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;

    public VendaRepositorio(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("Default");
    }

    public VendaRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirVenda(Venda venda)
    {
        _context.Vendas.Add(venda);
        _context.SaveChanges();
    }

    public Venda? ObterPorId(int id)
    {
        return _context.Vendas.Include(v => v.VendaProdutos).ThenInclude(vp => vp.Produto)
            .FirstOrDefault(v => v.Id == id);
    }

    public List<Venda> ObterVendas()
    {
        return new List<Venda>(_context.Vendas.Include(v => v.VendaProdutos)
                .ThenInclude(vp => vp.Produto))
            .ToList();
    }

    public void ExcluirVenda(Venda venda)
    {
        _context.Vendas.Remove(venda);
        _context.SaveChanges();
    }

    public async Task<List<VendaMensalDto>> ObterVendasMensaisAsync()
    {
        var vendasMensais = new List<VendaMensalDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterVendasMensais", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var vendaMensal = new VendaMensalDto
                        {
                            MesAno = reader.GetString(reader.GetOrdinal("MesAno")),
                            QuantidadeVendas =
                                reader.GetInt32(reader.GetOrdinal("QuantidadeVendas")),
                            TotalMensal = (decimal)reader.GetDouble(reader.GetOrdinal("TotalMensal"))
                        };
                        vendasMensais.Add(vendaMensal);
                    }
                }
            }
        }

        return vendasMensais;
    }
}