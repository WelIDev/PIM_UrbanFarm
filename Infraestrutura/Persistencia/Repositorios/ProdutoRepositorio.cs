using System.Data;
using Aplicacao.DTOs;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Persistencia.Repositorios;

public class ProdutoRepositorio : IProdutoRepositorio
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;

    public ProdutoRepositorio(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("Default");
    }

    public void InserirProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();
    }

    public Produto? ObterPorId(int id)
    {
        return _context.Produtos.Find(id);
    }

    public IList<Produto> ObterProdutosPorId(IList<int> produtoIds)
    {
        return _context.Produtos.Where(p => produtoIds.Contains(p.Id)).ToList();
    }

    public List<Produto> ObterProdutos()
    {
        return _context.Produtos.ToList();
    }

    public void ExcluirProduto(Produto produto)
    {
        _context.Produtos.Remove(produto);
    }

    public void AlterarProduto(Produto produto)
    {
        _context.Produtos.Update(produto);
    }

    public async Task AtualizarEstoqueAsync(int id, int quantidade)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            produto.Estoque += quantidade;
            _context.Produtos.Update(produto);
        }
    }

    public async Task<List<ProdutosMaisVendidosDto>> ObterProdutosMaisVendidosAsync()
    {
        var produtosMaisVendidos = new List<ProdutosMaisVendidosDto>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(
                       @" SELECT P.Nome, SUM(VP.Quantidade) AS TotalVendido FROM VendaProduto VP INNER JOIN Produtos P ON VP.IdProduto = P.Id GROUP BY P.Nome ORDER BY TotalVendido DESC;",
                       connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var produtoMaisVendido = new ProdutosMaisVendidosDto
                        {
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            TotalVendido = reader.GetInt32(reader.GetOrdinal("TotalVendido"))
                        };
                        produtosMaisVendidos.Add(produtoMaisVendido);
                    }
                }
            }
        }

        return produtosMaisVendidos;
    }

    public async Task<List<ProdutoVendasDto>> ObterVendasPorProdutoAsync()
    {
        var produtosVendas = new List<ProdutoVendasDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterVendasPorProduto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var produtoVendas = new ProdutoVendasDto
                        {
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            QuantidadeVendida =
                                reader.GetInt32(reader.GetOrdinal("QuantidadeVendida")),
                            Faturamento = reader.GetDecimal(reader.GetOrdinal("Faturamento"))
                        };
                        produtosVendas.Add(produtoVendas);
                    }
                }
            }
        }

        return produtosVendas;
    }

    public async Task<List<ProdutoEstoqueDto>> ObterNiveisEstoqueAsync()
    {
        var niveisEstoque = new List<ProdutoEstoqueDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterNiveisEstoque", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var produtoEstoque = new ProdutoEstoqueDto
                        {
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Estoque = reader.GetInt32(reader.GetOrdinal("Estoque"))
                        };
                        niveisEstoque.Add(produtoEstoque);
                    }
                }
            }
        }

        return niveisEstoque;
    }

    public async Task<List<ProdutoDto>> ObterUltimosProdutosAsync()
    {
        var produtosRecentes = new List<ProdutoDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterUltimosProdutos", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var produtoRecente = new ProdutoDto
                        {
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                            Estoque = reader.GetInt32(reader.GetOrdinal("Estoque")),
                            Descricao = reader.GetString(reader.GetOrdinal("Descricao"))
                        };
                        produtosRecentes.Add(produtoRecente);
                    }
                }
            }
        }

        return produtosRecentes;
    }

    public async Task<List<ProdutoVendaCustoDto>> ObterVendasCustosProdutosAsync()
    {
        var produtosVendasCustos = new List<ProdutoVendaCustoDto>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterVendasCustosProdutos", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var produto = new ProdutoVendaCustoDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            TotalVendas = reader.IsDBNull(reader.GetOrdinal("TotalVendas"))
                                ? 0
                                : reader.GetDecimal(reader.GetOrdinal("TotalVendas")),
                            TotalCusto = reader.IsDBNull(reader.GetOrdinal("TotalCusto"))
                                ? 0
                                : reader.GetDecimal(reader.GetOrdinal("TotalCusto"))
                        };
                        produtosVendasCustos.Add(produto);
                    }
                }
            }
        }

        return produtosVendasCustos;
    }

    public async Task<ResumoFinanceiroDto> ObterResumoFinanceiroAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterResumoFinanceiro", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ResumoFinanceiroDto
                        {
                            TotalEntradas =
                                reader.GetDecimal(reader.GetOrdinal("TotalEntradas")),
                            TotalSaidas = reader.GetDecimal(reader.GetOrdinal("TotalSaidas")),
                            Balanco = reader.GetDecimal(reader.GetOrdinal("Balanco")),
                            TotalSalarios =
                                reader.GetDecimal(reader.GetOrdinal("TotalSalarios"))
                        };
                    }
                }
            }
        }

        return null;
    }

    public async Task<List<TransacaoDto>> ObterDetalhesEntradasAsync()
    {
        var detalhesEntradas = new List<TransacaoDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterDetalhesEntradas", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var transacao = new TransacaoDto
                        {
                            Data = reader.GetDateTime(reader.GetOrdinal("Data")),
                            Produto = reader.GetString(reader.GetOrdinal("Produto")),
                            Valor = reader.GetDecimal(reader.GetOrdinal("Valor")),
                            Fornecedor = reader.GetString(reader.GetOrdinal("Fornecedor"))
                        };
                        detalhesEntradas.Add(transacao);
                    }
                }
            }
        }

        return detalhesEntradas;
    }

    public async Task<List<MovimentacaoMonetariaDto>> ObterMovimentacoesMonetariasAsync()
    {
        var movimentacoes = new List<MovimentacaoMonetariaDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("ObterMovimentacoesMonetarias", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var movimentacao = new MovimentacaoMonetariaDto
                        {
                            Data = reader.GetDateTime(reader.GetOrdinal("Data")),
                            Valor = reader.GetDecimal(reader.GetOrdinal("Valor")),
                            Tipo = reader.GetString(reader.GetOrdinal("Tipo"))
                        };
                        movimentacoes.Add(movimentacao);
                    }
                }
            }
        }

        return movimentacoes;
    }
}
