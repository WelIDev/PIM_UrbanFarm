using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class MenuController : Controller
{
    private readonly ILogger<MenuController> _logger;

    public MenuController(ILogger<MenuController> logger)
    {
        _logger = logger;
    }

    public IActionResult MenuPrincipal()
    {
        return View();
    }

    public IActionResult AdicionarCliente()
    {
        return View("Views/Cliente/AdicionarCliente.cshtml");
    }

    public IActionResult GestaoDeClientes()
    {
        return View("Views/Cliente/GestaoDeClientes.cshtml");
    }

    public IActionResult AdicionarFornecedor()
    {
        return View("Views/Fornecedor/AdicionarFornecedor.cshtml");
    }

    public IActionResult GestaoDeFornecedores()
    {
        return View("Views/Fornecedor/GestaoDeFornecedores.cshtml");
    }

    public IActionResult AdicionarUsuario()
    {
        return View("Views/Usuario/AdicionarUsuario.cshtml");
    }

    public IActionResult GerenciamentoPerfis()
    {
        return View("Views/Usuario/GerenciamentoPerfis.cshtml");
    }

    public IActionResult Configuracoes()
    {
        return View();
    }
    public IActionResult Contatos()
    {
        return View();
    }

    public IActionResult Sobre()
    {
        return View();
    }
}

