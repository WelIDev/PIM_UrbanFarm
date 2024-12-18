﻿using Dominio.Entidades;
using Infraestrutura.Persistencia.Configuracoes;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Persistencia;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<HistoricoCompra> HistoricoCompras { get; set; }
    public DbSet<AbastecimentoEstoque> AbastecimentosEstoque { get; set; }
    public DbSet<VendaProduto> VendaProduto { get; set; }
    public DbSet<ItemAbastecimento> ItensAbastecimento { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracao());
        modelBuilder.ApplyConfiguration(new FornecedorConfiguracao());
        modelBuilder.ApplyConfiguration(new ClienteConfiguracao());
        modelBuilder.ApplyConfiguration(new EnderecoConfiguracao());
        modelBuilder.ApplyConfiguration(new ProdutoConfiguracao());
        modelBuilder.ApplyConfiguration(new VendedorConfiguracao());
        modelBuilder.ApplyConfiguration(new VendaConfiguracao());
        modelBuilder.ApplyConfiguration(new AbastecimentoEstoqueConfiguracao());
        modelBuilder.ApplyConfiguration(new VendaProdutoConfiguracao());
        modelBuilder.ApplyConfiguration(new ItemAbastecimentoConfiguracao());
    }
}
