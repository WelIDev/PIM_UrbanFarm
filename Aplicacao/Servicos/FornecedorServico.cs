using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacao.Servicos;

public class FornecedorServico : IFornecedorServico
{
    private readonly IFornecedorRepositorio _fornecedorRepositorio;

    public FornecedorServico(IFornecedorRepositorio fornecedorRepositorio)
    {
        _fornecedorRepositorio = fornecedorRepositorio;
    }

    public bool Inserir(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        if (string.IsNullOrWhiteSpace(fornecedor.Nome))
        {
            throw new ArgumentException("Nome do fornecedor é obrigatório.");
        }

        try
        {
            _fornecedorRepositorio.InserirFornecedor(fornecedor);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public Fornecedor ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var fornecedor = _fornecedorRepositorio.ObterPorId(id);
        if (fornecedor == null)
        {
            throw new KeyNotFoundException("Fornecedor não encontrado.");
        }

        return fornecedor;
    }

    public List<Fornecedor> ObterFornecedores()
    {
        var fornecedores = _fornecedorRepositorio.ObterFornecedores();
        if (fornecedores == null || fornecedores.Count == 0)
        {
            throw new KeyNotFoundException("Nenhum fornecedor encontrado.");
        }

        return fornecedores;
    }

    public bool AlterarFornecedor(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        if (string.IsNullOrWhiteSpace(fornecedor.Nome))
        {
            throw new ArgumentException("Nome do fornecedor é obrigatório.");
        }

        try
        {
            _fornecedorRepositorio.AtualizarFornecedor(fornecedor);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar : " + e.Message);
            return false;
        }
    }

    public bool ExcluirFornecedor(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var fornecedor = _fornecedorRepositorio.ObterPorId(id);
        try
        {
            if (fornecedor != null)
            {
                _fornecedorRepositorio.ExcluirFornecedor(fornecedor);
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
