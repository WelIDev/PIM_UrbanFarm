namespace WebApp.Models;

public class UsuarioModel
{
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Funcao { get; set; }
        public DateTime DataCriacao { get; set; }


        public string FuncaoString => Funcao == "1" ? "Vendedor" : "Administrador";
}
