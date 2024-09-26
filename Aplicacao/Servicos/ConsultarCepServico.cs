using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacao.Servicos;

public class ConsultarCepServico : IConsultarCepServico
{
    private readonly ICepServico _cepServico;

    public ConsultarCepServico(ICepServico cepServico)
    {
        _cepServico = cepServico;
    }

    public Endereco Execute(string cep)
    {
        return _cepServico.ConsultarCep(cep);
    }
}