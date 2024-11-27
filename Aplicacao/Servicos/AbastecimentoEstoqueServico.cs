using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class AbastecimentoEstoqueServico : IAbastecimentoEstoqueServico
{
    private readonly IAbastecimentoEstoqueRepositorio _abastecimentoEstoqueRepositorio;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProdutoRepositorio _produtoRepositorio;

    public AbastecimentoEstoqueServico(
        IAbastecimentoEstoqueRepositorio abastecimentoEstoqueRepositorio, IUnitOfWork unitOfWork,
        IProdutoRepositorio produtoRepositorio)
    {
        _abastecimentoEstoqueRepositorio = abastecimentoEstoqueRepositorio;
        _unitOfWork = unitOfWork;
        _produtoRepositorio = produtoRepositorio;
    }

    public async Task<bool> InserirAbastecimentoEstoque(
        AbastecimentoEstoqueDto abastecimentoEstoqueDto)
    {
        ArgumentNullException.ThrowIfNull(abastecimentoEstoqueDto);

        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var abastecimentoEstoque = new AbastecimentoEstoque
            {
                FornecedorId = abastecimentoEstoqueDto.FornecedorId,
                UsuarioId = abastecimentoEstoqueDto.UsuarioId,
                Observacoes = abastecimentoEstoqueDto.Observacoes
            };

            _abastecimentoEstoqueRepositorio.InserirAbastecimentoEstoque
                (abastecimentoEstoque);
            await _unitOfWork.SaveChangesAsync();

            foreach (var produtoDto in abastecimentoEstoqueDto.Produtos)
            {
                var produto = _produtoRepositorio.ObterPorId(produtoDto.ProdutoId);
                if (produto == null)
                {
                    throw new Exception("Produto não encontrado");
                }

                produto.Estoque += produtoDto.Quantidade;
                _produtoRepositorio.AlterarProduto(produto);

                var itemAbastecimento = new ItemAbastecimento
                {
                    AbastecimentoEstoqueId = abastecimentoEstoque.Id,
                    ProdutoId = produtoDto.ProdutoId,
                    Quantidade = produtoDto.Quantidade,
                    Custo = produtoDto.Custo
                };
                abastecimentoEstoque.ItensAbastecimento.Add(itemAbastecimento);
            }

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
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
