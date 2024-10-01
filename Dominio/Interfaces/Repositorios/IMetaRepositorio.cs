using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IMetaRepositorio
{
    void InserirMeta(Meta meta);
    Meta? ObterPorId(int id);
    List<Meta> ObterMetas();
    void AlterarMeta(Meta meta);
    void ExcluirMeta(Meta meta);
}
