namespace Dominio.Entidades;

public class VendaProduto
{
        public int VendaId { get; set; }
        public Venda Venda { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
    
    }

