using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Infraestrutura.Persistencia.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly AppDbContext _context;

    public UsuarioRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public Usuario? ObterPorEmail(string email)
    {
        return _context.Usuarios.Find(email);
    }

    public Usuario? ObterPorId(int id)
    {
        return _context.Usuarios.Find(id);
    }

    public List<Usuario> ObterUsuarios()
    {
        return _context.Usuarios.ToList();
    }

    public void ExcluirUsuario(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
    }
}