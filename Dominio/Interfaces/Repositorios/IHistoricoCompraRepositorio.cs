﻿using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IHistoricoCompraRepositorio
{
    void InserirHistorico(HistoricoCompra? historicoCompra);
    HistoricoCompra? ObterPorId(int id);
    List<HistoricoCompra> ObterHistoricosCompras();
    void AlterarHistorico(HistoricoCompra historicoCompra);
    void ExcluirHistorico(HistoricoCompra historicoCompra);
}