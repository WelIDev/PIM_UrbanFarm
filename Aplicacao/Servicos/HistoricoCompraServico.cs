using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class HistoricoCompraServico : IHistoricoCompraServico
{
    private readonly IHistoricoCompraRepositorio _historicoCompraRepositorio;

    public HistoricoCompraServico(IHistoricoCompraRepositorio historicoCompraRepositorio)
    {
        _historicoCompraRepositorio = historicoCompraRepositorio;
    }

    public bool InserirHistoricoCompra(HistoricoCompra historicoCompra)
    {
        ArgumentNullException.ThrowIfNull(historicoCompra);

        try
        {
            _historicoCompraRepositorio.InserirHistorico(historicoCompra);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir: " + e.Message);
            return false;
        }
    }

    public HistoricoCompra ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var historicoCompra = _historicoCompraRepositorio.ObterPorId(id);
        if (historicoCompra == null)
        {
            throw new KeyNotFoundException("Histórico de compra não encontrada.");
        }

        return historicoCompra;
    }

    public List<HistoricoCompra> ObterHistoricoCompras()
    {
        var historicosCompras = _historicoCompraRepositorio.ObterHistoricosCompras();
        if (historicosCompras == null || historicosCompras.Count == 0)
        {
            throw new KeyNotFoundException("Nenhum histórico encontrado.");
        }

        return historicosCompras;
    }

    public bool AlterarHistoricoCompra(HistoricoCompra historicoCompra)
    {
        ArgumentNullException.ThrowIfNull(historicoCompra);
        
        try
        {
            _historicoCompraRepositorio.AlterarHistorico(historicoCompra);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar: " + e.Message);
            return false;
        }
    }

    public bool ExcluirHistoricoCompra(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido");
        }

        var meta = _historicoCompraRepositorio.ObterPorId(id);
        try
        {
            if (meta != null)
            {
                _historicoCompraRepositorio.ExcluirHistorico(meta);
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
