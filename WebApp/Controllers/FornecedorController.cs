using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers;

public class FornecedorController : Controller
{
    private readonly ILogger<MenuController> _logger;
    private readonly HttpClient _httpClient;

    public FornecedorController(ILogger<MenuController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> GestaoDeFornecedores()
    {
        var response = await _httpClient.GetStringAsync
            ("https://localhost:7124/api/Fornecedor/ObterFornecedores");
        var fornecedores = JsonConvert.DeserializeObject<List<FornecedorModel>>(response);
        return View("GestaoDeFornecedores", fornecedores);
    }

    [HttpGet]
    public IActionResult AdicionarFornecedor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarFornecedor(FornecedorModel fornecedorModel)
    {
        if (!ModelState.IsValid)
        {
            return View("AdicionarFornecedor", fornecedorModel);
        }

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7124/api/Fornecedor/Inserir", fornecedorModel);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("MenuPrincipal");
        }
        return View("AdicionarFornecedor", fornecedorModel);
    }

    public async Task<IActionResult> ObterEndereco(string cep)
    {
        var endereco = await _httpClient.GetStringAsync($"https://localhost:7124/api/Cep/ConsultarCep/?cep={cep}");
        var resultado = JsonConvert.DeserializeObject<EnderecoModel>(endereco);

        return Json(resultado);
    }
}
