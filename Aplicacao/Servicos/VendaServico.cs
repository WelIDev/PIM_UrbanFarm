using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class VendaServico : IVendaServico
{
    private readonly IVendaRepositorio _vendaRepositorio;
    private readonly IProdutoRepositorio _produtoRepositorio;
    private readonly IUnitOfWork _unitOfWork;

    public VendaServico(IVendaRepositorio vendaRepositorio, IProdutoRepositorio produtoRepositorio, IUnitOfWork unitOfWork)
    {
        _vendaRepositorio = vendaRepositorio;
        _produtoRepositorio = produtoRepositorio;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> InserirVenda(VendaDto vendaDto)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var venda = new Venda
            {
                VendedorId = vendaDto.VendedorId,
                HistoricoCompraId = vendaDto.HistoricoCompraId,
                FormaDePagamento = vendaDto.FormaDePagamento,
                Valor = vendaDto.VendaProdutos.Sum(vp => vp.ValorTotal)
            };

            foreach (var produtoDto in vendaDto.VendaProdutos)
            {
                Produto produto = _produtoRepositorio.ObterPorId(produtoDto.IdProduto);
                if (produto == null)
                {
                    return false;
                }

                var vendaProduto = new VendaProduto
                {
                    IdProduto = produtoDto.IdProduto,
                    Quantidade = produtoDto.Quantidade,
                    ValorTotal = produto.Preco * produtoDto.Quantidade
                };

                venda.VendaProdutos.Add(vendaProduto);

                await _produtoRepositorio.AtualizarEstoqueAsync(produtoDto.IdProduto,
                    -produtoDto.Quantidade);
            }

            _unitOfWork.VendaRepositorio.InserirVenda(venda);
            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
    public VendaDto ObterPorId(int id)
    {
        var venda = _vendaRepositorio.ObterPorId(id);

        var vendaDTO = new VendaDto
        {
            Id = venda.Id,
            Data = venda.Data,
            Valor = venda.Valor,
            FormaDePagamento = venda.FormaDePagamento,
            VendedorId = venda.VendedorId,
            VendaProdutos = venda.VendaProdutos.Select(vp => new VendaProdutoDto
            {
                IdProduto = vp.Produto.Id,
                NomeProduto = vp.Produto.Nome,
                Quantidade = vp.Quantidade,
                ValorTotal = vp.ValorTotal
            }).ToList()
        };

        return vendaDTO;
    }

    public List<Venda> ObterVendas()
    {
        var vendas = _vendaRepositorio.ObterVendas();
        if (vendas.Count == 0)
        {
            throw new KeyNotFoundException("Nenhuma venda encontrada.");
        }

        return vendas;
    }

    public bool ExcluirVenda(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var venda = _vendaRepositorio.ObterPorId(id);
        try
        {
            if (venda != null)
            {
                _vendaRepositorio.ExcluirVenda(venda);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar excluir: " + e.Message);
            return false;
        }
    }

    public async Task<List<VendaMensalDto>> ObterVendasMensaisAsync()
    {
        return await _vendaRepositorio.ObterVendasMensaisAsync();
    }
}
