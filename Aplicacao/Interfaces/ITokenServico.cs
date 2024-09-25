using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface ITokenServico
{
    string GerarToken(Usuario usuario);
}