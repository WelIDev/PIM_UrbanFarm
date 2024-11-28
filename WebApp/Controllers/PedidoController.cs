using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class PedidoController : Controller
{
    private readonly HttpClient _httpClient;

    public PedidoController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IActionResult MenuPrincipal()
    {
        return View("GestaoDePedidos");
    }

    [HttpGet]
    public async Task<JsonResult> ObterProdutos()
    {
        var produtos = new List<ProdutoModel>();
        try
        {
            var response = await _httpClient.GetAsync("api/Produto/ObterProdutos");

            if (response.IsSuccessStatusCode)
            {
                produtos = await response.Content.ReadFromJsonAsync<List<ProdutoModel>>();
                return Json(produtos);
            }
            else
            {
                return Json(new { error = "Erro ao obter produtos." });
            }
        }
        catch (HttpRequestException e)
        {
            return Json(new { error = $"Erro na requisição: {e.Message}" });
        }
    }
}
