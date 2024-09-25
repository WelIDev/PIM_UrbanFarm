using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface ISegurancaServico
{
    string HashSenha(Usuario usuario, string senha);
    bool VerificarSenha(Usuario usuario, string senha);
}