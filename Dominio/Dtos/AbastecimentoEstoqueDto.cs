namespace Aplicacao.DTOs;

public class AbastecimentoEstoqueDto
{
    public int Id { get; set; }
    public List<ProdutoAbastecimentoDto> Produtos { get; set; }
    public int FornecedorId { get; set; }
    public int UsuarioId { get; set; }
    public string Observacoes { get; set; }
}
