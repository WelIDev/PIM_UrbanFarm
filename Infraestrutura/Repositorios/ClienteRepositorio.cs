using System.Data;
using System.Data.SqlClient;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Repositorios;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly string _connectionString;

    public ClienteRepositorio(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ??
                            throw new InvalidOperationException();
    }

    public bool InserirCliente(Cliente cliente)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command =
                       new SqlCommand("inserir_cliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@CpfCnpj", cliente.CpfCnpj);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);

                    command.Parameters.AddWithValue("@Rua", cliente.Endereco.Rua);
                    command.Parameters.AddWithValue("@Bairro", cliente.Endereco.Bairro);
                    command.Parameters.AddWithValue("@Cidade", cliente.Endereco.Cidade);
                    command.Parameters.AddWithValue("@Estado", cliente.Endereco.Estado);
                    command.Parameters.AddWithValue("@Cep", cliente.Endereco.Cep);

                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return false;
                    }

                    connection.Close();
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro: " + e.Message);
            throw;
        }
    }

    public Cliente? ObterPorId(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("obter_cliente_por_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ClienteId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Email = reader.GetString(2),
                                CpfCnpj = reader.GetString(3),
                                Telefone = reader.GetString(4),
                                DataNascimento = reader.GetDateTime(5),
                                Endereco = new Endereco
                                {
                                    Rua = reader.GetString(6),
                                    Bairro = reader.GetString(7),
                                    Cidade = reader.GetString(8),
                                    Estado = reader.GetString(9),
                                    Cep = reader.GetString(10)
                                }
                            };
                        }

                        reader.Close();
                        connection.Close();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro: " + e.Message);
            throw;
        }

        return null;
    }

    public List<Cliente> ObterClientes()
    {
        var clientes = new List<Cliente>();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("obter_clientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var cliente = new Cliente
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            CpfCnpj = reader.GetString(3),
                            Telefone = reader.GetString(4),
                            DataNascimento = reader.GetDateTime(5),
                            Endereco = new Endereco
                            {
                                Rua = reader.GetString(6),
                                Bairro = reader.GetString(7),
                                Cidade = reader.GetString(8),
                                Estado = reader.GetString(9),
                                Cep = reader.GetString(10)
                            }
                        };
                        clientes.Add(cliente);
                    }

                    reader.Close();
                    connection.Close();
                }
            }

            return clientes;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro: " + e.Message);
            throw;
        }
    }

    public bool ExcluirCliente(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("excluir_cliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ClienteId", id);

                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return false;
                    }

                    connection.Close();
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro: " + e.Message);
            throw;
        }
    }

    public bool AtualizarCliente(Cliente cliente)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString: _connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("atualizar_cliente", connection: connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(parameterName: "@ClienteId", value: cliente.Id);
                    command.Parameters.AddWithValue(parameterName: "@Nome", value: cliente.Nome);
                    command.Parameters.AddWithValue(parameterName: "@Email", value: cliente.Email);
                    command.Parameters.AddWithValue(parameterName: "@CpfCnpj",
                        value: cliente.CpfCnpj);
                    command.Parameters.AddWithValue(parameterName: "@Telefone",
                        value: cliente.Telefone);
                    command.Parameters.AddWithValue(parameterName: "@DataNascimento",
                        value: cliente.DataNascimento);
                    command.Parameters.AddWithValue(parameterName: "@Rua",
                        value: cliente.Endereco.Rua);
                    command.Parameters.AddWithValue(parameterName: "@Bairro",
                        value: cliente.Endereco.Bairro);
                    command.Parameters.AddWithValue(parameterName: "@Cidade",
                        value: cliente.Endereco.Cidade);
                    command.Parameters.AddWithValue(parameterName: "@Estado",
                        value: cliente.Endereco.Estado);
                    command.Parameters.AddWithValue(parameterName: "@Cep",
                        value: cliente.Endereco.Cep);

                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return false;
                    }

                    connection.Close();
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(value: "Ocorreu um erro: " + e.Message);
            throw;
        }
    }
}
