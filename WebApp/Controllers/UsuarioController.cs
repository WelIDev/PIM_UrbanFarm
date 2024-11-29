using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<MenuController> _logger;
    private readonly HttpClient _httpClient;

    public UsuarioController(ILogger<MenuController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> GerenciamentoPerfis()
    {
        var response = await _httpClient.GetStringAsync
            ("https://localhost:7124/api/Usuario/ObterUsuarios");
        var usuarios = JsonConvert.DeserializeObject<List<UsuarioModel>>(response);
        return View("GerenciamentoPerfis", usuarios);
    }

    [HttpGet]
    public IActionResult AdicionarUsuario()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarUsuario(UsuarioModel usuarioModel)
    {
        if (!ModelState.IsValid)
        {
            return View("AdicionarUsuario", usuarioModel);
        }

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7124/api/Usuario/InserirUsuario", usuarioModel);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GerenciamentoPerfis");
        }
        return View("AdicionarUsuario", usuarioModel);
    }
}
