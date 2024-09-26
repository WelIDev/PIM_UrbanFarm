using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface ICepServico
{
    Endereco ConsultarCep(string cep);
}