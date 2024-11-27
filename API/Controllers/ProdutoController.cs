using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProdutoController : Controller
{
    private readonly IProdutoServico _produtoServico;

    public ProdutoController(IProdutoServico produtoServico)
    {
        _produtoServico = produtoServico;
    }

    [HttpPost]
    public ActionResult Inserir(Produto produto)
    {
        try
        {
            if (_produtoServico.InserirProduto(produto))
            {
                return Ok("Produto incluído com sucesso! Id: " + produto.Id);
            }

            return BadRequest("Não foi possível inserir o produto");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir o produto.");
        }
    }

    [HttpGet]
    public ActionResult ObterPorId(int id)
    {
        try
        {
            var produto = _produtoServico.ObterPorId(id);
            return Ok(produto);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter o produto.");
        }
    }

    [HttpGet]
    public ActionResult ObterProdutos()
    {
        try
        {
            var produtos = _produtoServico.ObterProdutos().Select(p => new
            {
                p.Id,
                p.Nome,
                p.Descricao,
                p.Preco
            });
            return Ok(produtos);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter todos produtos.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ObterProdutosMaisVendidos()
    {
        var produtos = await _produtoServico.ObterProdutosMaisVendidosAsync();
        return Ok(produtos);
    }

    [HttpGet]
    public async Task<IActionResult> ObterVendasPorProduto()
    {
        var produtos = await _produtoServico.ObterVendasPorProdutoAsync();
        return Ok(produtos);
    }

    [HttpGet]
    public async Task<IActionResult> ObterNiveisEstoque()
    {
        var niveisEstoque = await _produtoServico.ObterNiveisEstoqueAsync();
        return Ok(niveisEstoque);
    }

    [HttpGet]
    public async Task<IActionResult> ObterUltimosProdutos()
    {
        var produtosRecentes = await _produtoServico.ObterUltimosProdutosAsync();
        return Ok(produtosRecentes);
    }

    [HttpGet]
    public async Task<IActionResult> ObterMargemLucroProdutos()
    {
        var produtosLucro = await _produtoServico.ObterMargemLucroProdutosAsync();
        return Ok(produtosLucro);
    }

    [HttpGet]
    public async Task<IActionResult> ObterResumoFinanceiro()
    {
        var resumoFinanceiro = await _produtoServico.ObterResumoFinanceiroAsync();
        return Ok(resumoFinanceiro);
    }

    [HttpGet]
    public async Task<IActionResult> ObterDetalhesEntradas()
    {
        var detalhesEntradas = await _produtoServico.ObterDetalhesEntradasAsync();
        return Ok(detalhesEntradas);
    }

    [HttpGet]
    public async Task<IActionResult> ObterMovimentacoesMonetarias()
    {
        var movimentacoes = await _produtoServico.ObterMovimentacoesMonetariasAsync();
        return Ok(movimentacoes);
    }

    [HttpPut]
    public ActionResult AlterarProduto(Produto produto)
    {
        try
        {
            if (_produtoServico.AlterarProduto(produto))
            {
                return Ok();
            }

            return BadRequest("Não foi possível alterar o produto");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar alterar o produto.");
        }
    }

    [HttpDelete]
    public ActionResult ExcluirProduto(int id)
    {
        try
        {
            if (_produtoServico.ExcluirProduto(id))
            {
                return Ok();
            }

            return NotFound("Produto não encontrado.");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar excluir o produto.");
        }
    }
}
