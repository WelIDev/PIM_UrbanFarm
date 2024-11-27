using System.Net.Http;
using System.Text;
using DesktopApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DesktopApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseAddress = "https://localhost:7124/api/";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<ProdutoModel>> GetProdutosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseAddress}Produto/ObterProdutos");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var produtosJObjects = JObject.Parse(jsonResponse);
                var produtosArray = produtosJObjects["$values"].ToObject<List<ProdutoModel>>();
                return produtosArray;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<ProdutoModel>();
            }
        }

        public async Task<List<FornecedorModel>> GetFornecedoresAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Fornecedor/ObterFornecedores");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var fornecedoresJObjects = JObject.Parse(jsonResponse);

                var fornecedoresArray =
                    fornecedoresJObjects["$values"].ToObject<List<FornecedorModel>>();
                return fornecedoresArray;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<FornecedorModel>();
            }
        }

        public async Task<bool> CadastrarProdutoAsync(ProdutoModel produto)
        {
            var content = new StringContent(JsonSerializer.Serialize(produto), Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync($"{BaseAddress}Produto/Inserir", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CadastrarFornecedorAsync(FornecedorModel fornecedor)
        {
            var content = new StringContent(JsonSerializer.Serialize(fornecedor), Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync($"{BaseAddress}Fornecedor/Inserir", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CadastrarAbastecimentoAsync(AbastecimentoModel abastecimento)
        {
            var content = new StringContent(JsonSerializer.Serialize(abastecimento), Encoding.UTF8,
                "application/json");
            var response =
                await _httpClient.PostAsync(
                    $"{BaseAddress}AbastecimentoEstoque/InserirAbastecimentoEstoque", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<VendedorModel>?> ObterVendedoresAsync(DateTime? dataInicio, DateTime?
            dataFim)
        {
            try
            {
                var response = await _httpClient.GetAsync
                    ($"{BaseAddress}Vendedor/ObterVendedoresComVendas?dataInicio={dataInicio:yyyy-MM-dd}&dataFim={dataFim:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(
                    jsonResponse);
                var vendedoresJObject = JObject.Parse(jsonResponse);
                var vendedoresArray = vendedoresJObject["$values"].ToObject<List<VendedorModel>>();
                return vendedoresArray ?? new List<VendedorModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<VendedorModel>();
            }
        }

        public async Task<List<ProdutoMaisVendidoModel>> ObterProdutosMaisVendidosAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterProdutosMaisVendidos");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var produtosMaisVendidos =
                    JsonConvert.DeserializeObject<List<ProdutoMaisVendidoModel>>(jsonResponse);
                return produtosMaisVendidos ?? new List<ProdutoMaisVendidoModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<ProdutoMaisVendidoModel>();
            }
        }

        public async Task<List<ProdutoVendasModel>> ObterVendasPorProdutoAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterVendasPorProduto");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var produtosVendas =
                    JsonConvert.DeserializeObject<List<ProdutoVendasModel>>(jsonResponse);
                return produtosVendas ?? new List<ProdutoVendasModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<ProdutoVendasModel>();
            }
        }

        public async Task<List<VendaMensalModel>> ObterVendasMensaisAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseAddress}Venda/ObterVendasMensais");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var vendasMensais =
                    JsonConvert.DeserializeObject<List<VendaMensalModel>>(jsonResponse);
                return vendasMensais ?? new List<VendaMensalModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<VendaMensalModel>();
            }
        }

        public async Task<List<ProdutoEstoqueModel>> ObterNiveisEstoqueAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterNiveisEstoque");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var niveisEstoque =
                    JsonConvert.DeserializeObject<List<ProdutoEstoqueModel>>(jsonResponse);
                return niveisEstoque ?? new List<ProdutoEstoqueModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<ProdutoEstoqueModel>();
            }
        }

        public async Task<List<ProdutoModel>> ObterUltimosProdutosAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterUltimosProdutos");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var produtosRecentes =
                    JsonConvert.DeserializeObject<List<ProdutoModel>>(jsonResponse);
                return produtosRecentes ?? new List<ProdutoModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<ProdutoModel>();
            }
        }

        public async Task<List<ProdutoVendaCustoModel>> ObterMargemLucroProdutosAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterMargemLucroProdutos");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var produtosLucro =
                    JsonConvert.DeserializeObject<List<ProdutoVendaCustoModel>>(jsonResponse);
                return produtosLucro ?? new List<ProdutoVendaCustoModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<ProdutoVendaCustoModel>();
            }
        }

        public async Task<ResumoFinanceiroModel> ObterResumoFinanceiroAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterResumoFinanceiro");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResumoFinanceiroModel>(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return null;
            }
        }

        public async Task<List<TransacaoModel>> ObterDetalhesEntradasAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterDetalhesEntradas");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TransacaoModel>>(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<TransacaoModel>();
            }
        }

        public async Task<List<MovimentacaoMonetariaModel>> ObterMovimentacoesMonetariasAsync()
        {
            try
            {
                var response =
                    await _httpClient.GetAsync($"{BaseAddress}Produto/ObterMovimentacoesMonetarias");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MovimentacaoMonetariaModel>>(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<MovimentacaoMonetariaModel>();
            }
        }
    }
}
