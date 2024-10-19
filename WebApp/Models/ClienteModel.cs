namespace WebApp.Models;

public class ClienteModel
{
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public EnderecoModel Endereco { get; set; }
}
