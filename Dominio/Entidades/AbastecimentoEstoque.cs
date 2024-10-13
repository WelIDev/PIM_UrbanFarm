namespace Dominio.Entidades;

public class AbastecimentoEstoque
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }
    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public string Observacoes { get; set; }
}
