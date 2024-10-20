﻿namespace Dominio.Entidades;

public class Fornecedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cnpj { get; set; }
    public string InscricaoEstadual { get; set; }
    public string Telefone { get; set; }
    public int EnderecoId { get; set; }
    public Endereco Endereco { get; set; }
    public IList<Produto> Produtos { get; set; } = new List<Produto>();

    public IList<AbastecimentoEstoque> AbastecimentosEstoque { get; set; } =
        new List<AbastecimentoEstoque>();
}