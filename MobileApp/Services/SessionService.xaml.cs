using MobileApp.Models;

namespace MobileApp.Services;

public class SessionService
{
    private static readonly SessionService _instance = new SessionService();
    public static SessionService Instance => _instance;
    
    public UsuarioModel Usuario { get; private set; }

    public void SetUsuario(UsuarioModel usuarioModel)
    {
        Usuario = usuarioModel;
    }

    public void ClearUsuario()
    {
        Usuario = null;
    }
}
