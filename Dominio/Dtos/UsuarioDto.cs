namespace Aplicacao.DTOs;

public record UsuarioDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int Funcao { get; set; }
}