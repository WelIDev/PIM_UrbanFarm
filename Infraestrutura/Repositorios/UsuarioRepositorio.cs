using System.Data;
using System.Data.SqlClient;
using Dominio.Entidades;
using Dominio.Enums;
using Dominio.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly string _connectionString;

    public UsuarioRepositorio(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException();
    }

    public bool InserirUsuario(Usuario usuario)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("inserir_usuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nome", usuario.Nome);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Senha", usuario.Senha);
                    command.Parameters.AddWithValue("@Funcao", usuario.Funcao);

                    if (command.ExecuteNonQuery() > 0)
                    {
                        connection.Close();
                        return true;
                    }
                }
            }

            return false;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Ocorreu um erro: " + e.Message);
            throw;
        }
    }

    public Usuario? ObterPorEmail(string email)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("obter_usuario_por_email ", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Email = reader.GetString(2),
                                Senha = reader.GetString(3),
                                Funcao = (Funcao)reader.GetInt32(4),
                                DataCriacao = reader.GetDateTime(5)
                            };
                        }
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

    public Usuario? ObterPorId(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("obter_usuario_por_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Email = reader.GetString(2),
                                Senha = reader.GetString(3),
                                Funcao = (Funcao)reader.GetInt32(4),
                                DataCriacao = reader.GetDateTime(5)
                            };
                        }
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
    
    public List<Usuario> ObterUsuarios()
    {
        var usuarios = new List<Usuario>();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("obter_usuarios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var usuario = new Usuario()
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            Senha = reader.GetString(3),
                            Funcao = (Funcao)reader.GetInt32(4),
                            DataCriacao = reader.GetDateTime(5)
                        };
                        usuarios.Add(usuario);
                    }
                    reader.Close();
                    connection.Close();
                }
            }

            return usuarios;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro: " + e.Message);
            throw;
        }
    }

    public bool ExcluirUsuario(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("excluir_usuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    if (command.ExecuteNonQuery() > 0)
                    {
                        connection.Close();
                        return true;
                    }
                }
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro: " + e.Message);
            throw;
        }
    }
}