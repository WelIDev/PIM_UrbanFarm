using System.Data;
using System.Data.SqlClient;
using Aplicacao.DTOs;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Persistencia.Repositorios;

public class VendedorRepositorio : IVendedorRepositorio
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;

    public VendedorRepositorio(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("Default");
    }

    public void InserirVendedor(Vendedor vendedor)
    {
        _context.Vendedores.Add(vendedor);
        _context.SaveChanges();
    }

    public Vendedor? ObterPorId(int id)
    {
        return _context.Vendedores.Find(id);
    }

    public List<Vendedor?> ObterVendedores()
    {
        return _context.Vendedores.ToList();
    }

    public async Task<List<VendedorDto>> ObterVendedoresComVendasAsync(DateTime dataInicio,
        DateTime dataFim)
    {
        var vendedores = new List<VendedorDto>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("ObterVendedoresComVendas ", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DataInicio", dataInicio);
                command.Parameters.AddWithValue("@DataFim", dataFim);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var vendaId = reader.GetInt32(reader.GetOrdinal("VendaId"));
                        var dataVenda = reader.GetDateTime(reader.GetOrdinal("DataVenda"));
                        var valorVenda = reader.GetDouble(reader.GetOrdinal("ValorVenda"));
                        var vendedorId = reader.GetInt32(reader.GetOrdinal("VendedorId"));
                        var nomeVendedor = reader.GetString(reader.GetOrdinal("NomeVendedor"));

                        var vendedor = vendedores.Find(v => v.VendedorId == vendedorId);

                        if (vendedor == null)
                        {
                            vendedor = new VendedorDto
                            {
                                VendedorId = vendedorId,
                                NomeVendedor = nomeVendedor,
                                Vendas = new List<VendaVendedorDto>()
                            };
                            vendedores.Add(vendedor);
                        }
                        vendedor.Vendas.Add(new VendaVendedorDto
                        {
                            Data = dataVenda,
                            Valor = valorVenda
                        });
                    }
                }
            }
        }

        return vendedores;
    }

    public void AlterarVendedor(Vendedor vendedor)
    {
        _context.Vendedores.Update(vendedor);
        _context.SaveChanges();
    }

    public void ExcluirVendedor(Vendedor vendedor)
    {
        _context.Vendedores.Remove(vendedor);
        _context.SaveChanges();
    }
}
