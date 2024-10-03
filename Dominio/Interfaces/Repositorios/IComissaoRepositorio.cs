using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IComissaoRepositorio
{
    void InserirComissao(Comissao comissao);
    Comissao? ObterPorId(int id);
    List<Comissao> ObterComissoes();
    void ExcluirComissao(Comissao comissao);
    void AtualizarComissao(Comissao comissao);
}