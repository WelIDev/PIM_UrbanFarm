using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class MetaServico : IMetaServico
{
    private readonly IMetaRepositorio _metaRepositorio;

    public MetaServico(IMetaRepositorio metaRepositorio)
    {
        _metaRepositorio = metaRepositorio;
    }

    public bool InserirMeta(Meta meta)
    {
        ArgumentNullException.ThrowIfNull(meta);

        try
        {
            _metaRepositorio.InserirMeta(meta);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public Meta ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var meta = _metaRepositorio.ObterPorId(id);
        if (meta == null)
        {
            throw new KeyNotFoundException("Meta não encontrada.");
        }

        return meta;
    }

    public List<Meta> ObterMetas()
    {
        var metas = _metaRepositorio.ObterMetas();
        if (metas == null || metas.Count == 0)
        {
            throw new KeyNotFoundException("Nenhuma meta encontrada.");
        }

        return metas;
    }

    public bool AlterarMeta(Meta meta)
    {
        ArgumentNullException.ThrowIfNull(meta);
        
        try
        {
            _metaRepositorio.AlterarMeta(meta);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar: " + e.Message);
            return false;
        }
    }

    public bool ExcluirMeta(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var meta = _metaRepositorio.ObterPorId(id);
        try
        {
            if (meta != null)
            {
                _metaRepositorio.ExcluirMeta(meta);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar excluir: " + e.Message);
            return false;
        }
    }
}
