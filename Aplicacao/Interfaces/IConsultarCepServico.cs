using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IConsultarCepServico
{
    Endereco Execute(string cep);
}