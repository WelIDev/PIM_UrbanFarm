using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers;

public class ClienteController : Controller
{
    private readonly ILogger<MenuController> _logger;
    private readonly HttpClient _httpClient;

    public ClienteController(ILogger<MenuController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetStringAsync
            ("https://localhost:7124/api/Cliente/ObterClientes");
        var clientes = JsonConvert.DeserializeObject<List<ClienteModel>>(response);
        return View("GestaoDeClientes", clientes);
    }

    public IActionResult Adicionar()
    {
        return View();
    }

    public IActionResult Visualizar()
    {
        throw new NotImplementedException();
    }
}
