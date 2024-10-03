using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IComissaoServico
{
    bool InserirComissao(Comissao comissao);
    Comissao ObterPorId(int id);
    List<Comissao> ObterComissoes();
    bool AlterarComissao(Comissao comissao);
    bool ExcluirComissao(int id);
}
