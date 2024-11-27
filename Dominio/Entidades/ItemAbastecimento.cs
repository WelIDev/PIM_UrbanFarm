namespace Dominio.Entidades
{
    public class ItemAbastecimento
    {
        public int Id { get; set; }
        public int AbastecimentoEstoqueId { get; set; }
        public AbastecimentoEstoque AbastecimentoEstoque { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public double Custo { get; set; }
    }
}
