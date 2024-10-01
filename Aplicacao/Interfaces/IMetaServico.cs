using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IMetaServico
{
    bool InserirMeta(Meta meta);
    Meta ObterPorId(int id);
    List<Meta> ObterMetas();
    bool AlterarMeta(Meta meta);
    bool ExcluirMeta(int id);
}
