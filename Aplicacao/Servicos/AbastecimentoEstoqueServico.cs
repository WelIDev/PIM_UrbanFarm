using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class AbastecimentoEstoqueServico : IAbastecimentoEstoqueServico
{
    private readonly IAbastecimentoEstoqueRepositorio _abastecimentoEstoqueRepositorio;

    public AbastecimentoEstoqueServico(IAbastecimentoEstoqueRepositorio abastecimentoEstoqueRepositorio)
    {
        _abastecimentoEstoqueRepositorio = abastecimentoEstoqueRepositorio;
    }

    public bool InserirAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque)
    {
        ArgumentNullException.ThrowIfNull(abastecimentoEstoque);

        try
        {
            _abastecimentoEstoqueRepositorio.AlterarAbastecimentoEstoque(abastecimentoEstoque);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public AbastecimentoEstoque ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var abastecimentoEstoque = _abastecimentoEstoqueRepositorio.ObterPorId(id);
        if (abastecimentoEstoque == null)
        {
            throw new KeyNotFoundException("Abastecimento de estoque não encontrado.");
        }

        return abastecimentoEstoque;
    }

    public IList<AbastecimentoEstoque> ObterAbastecimentoEstoque()
    {
        var abastecimentoEstoque = _abastecimentoEstoqueRepositorio.ObterAbastecimentoEstoque();
        if (abastecimentoEstoque == null || abastecimentoEstoque.Count == 0)
        {
            throw new KeyNotFoundException("Nenhum abastecimento de estoque encontrada.");
        }

        return abastecimentoEstoque;
    }

    public bool AlterarAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque)
    {
        ArgumentNullException.ThrowIfNull(abastecimentoEstoque);
        
        try
        {
            _abastecimentoEstoqueRepositorio.AlterarAbastecimentoEstoque(abastecimentoEstoque);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar: " + e.Message);
            return false;
        }
    }

    public bool ExcluirAbastecimentoEstoque(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var abastecimentoEstoque = _abastecimentoEstoqueRepositorio.ObterPorId(id);
        try
        {
            if (abastecimentoEstoque != null)
            {
                _abastecimentoEstoqueRepositorio.ExcluirAbastecimentoEstoque(abastecimentoEstoque);
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
