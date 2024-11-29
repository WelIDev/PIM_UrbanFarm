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

    [HttpGet]
    public IActionResult AdicionarCliente()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(ClienteModel clienteModel)
    {
        if (!ModelState.IsValid)
        {
            return View("AdicionarCliente", clienteModel);
        }

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7124/api/Cliente/InserirCliente", clienteModel);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View("AdicionarCliente",clienteModel);
    }

    public async Task<IActionResult> ObterEndereco(string cep)
    {
        var endereco = await _httpClient.GetStringAsync($"https://localhost:7124/api/Cep/ConsultarCep/?cep={cep}");
        var resultado = JsonConvert.DeserializeObject<EnderecoModel>(endereco);

        return Json(resultado);
    }
}
