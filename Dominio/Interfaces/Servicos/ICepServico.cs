using Dominio.Entidades;

namespace Dominio.Interfaces.Servicos;

public interface ICepServico
{
    Endereco ConsultarCep(string cep);
}