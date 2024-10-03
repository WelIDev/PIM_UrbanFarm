using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class ComissaoServico : IComissaoServico
{
    private readonly IComissaoRepositorio _comissaoRepositorio;

    public ComissaoServico(IComissaoRepositorio comissaoRepositorio)
    {
        _comissaoRepositorio = comissaoRepositorio;
    }

    public bool InserirComissao(Comissao comissao)
    {
        ArgumentNullException.ThrowIfNull(comissao);
        try
        {
            _comissaoRepositorio.InserirComissao(comissao);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public Comissao ObterPorId(int id)
    {
        if (id <= 0 )
        {
            throw new ArgumentException("ID Inválido");
        }

        var comissao = _comissaoRepositorio.ObterPorId(id);
        if (comissao == null)
        {
            throw new KeyNotFoundException("Comissão não encontrada.");
        }

        return comissao;
    }

    public List<Comissao> ObterComissoes()
    {
        var comissoes = _comissaoRepositorio.ObterComissoes();
        if (comissoes == null || comissoes.Count == 0)
        {
            throw new KeyNotFoundException("Nenhuma comissão encontrada");
        }

        return comissoes;
    }

    public bool AlterarComissao(Comissao comissao)
    {
        ArgumentNullException.ThrowIfNull(comissao);

        try
        {
            _comissaoRepositorio.AtualizarComissao(comissao);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar: " + e.Message);
            return false;
        }
    }

    public bool ExcluirComissao(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var comissao = _comissaoRepositorio.ObterPorId(id);

        try
        {
            if (comissao != null)
            {
                _comissaoRepositorio.ExcluirComissao(comissao);
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
