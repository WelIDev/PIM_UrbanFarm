using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MobileApp.Models;

namespace MobileApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://192.168.1.2:7124/api";

        public ApiService()
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _httpClient = new HttpClient(handler);
        }

        public async Task<ObservableCollection<ProdutoModel>> GetProdutosAsync(string endpoint)
        {
            var apiUrl = $"{BaseUrl}{endpoint}";
            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);

                var jsonResponse = JsonSerializer.Deserialize<JsonResponse<ProdutoModel>>(response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                        IgnoreReadOnlyProperties = true
                    });

                var produtos = jsonResponse.Values;
                return new ObservableCollection<ProdutoModel>(produtos);
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Erro ao fazer a requisição HTTP: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                throw new Exception($"Erro ao desserializar a resposta JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro desconhecido: {ex.Message}");
            }
        }

        public async Task<ObservableCollection<ClienteModel>> GetClientesAsync(string endpoint)
        {
            var apiUrl = $"{BaseUrl}{endpoint}";
            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);

                var jsonResponse = JsonSerializer.Deserialize<JsonResponse<ClienteModel>>(response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                        IgnoreReadOnlyProperties = true
                    });

                var clientes = jsonResponse.Values;
                return new ObservableCollection<ClienteModel>(clientes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ObservableCollection<VendaModel>> GetVendasAsync(int clienteId,
            string endpoint)
        {
            var apiUrl = $"{BaseUrl}{endpoint}/{clienteId}";
            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                var jsonResponse = JsonSerializer.Deserialize<ClienteVendasModel>(response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                        IgnoreReadOnlyProperties = true
                    });
                return new ObservableCollection<VendaModel>(jsonResponse.Vendas.Values);
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Erro ao fazer a requisição HTTP: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                throw new Exception($"Erro ao desserializar a resposta JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro desconhecido: {ex.Message}");
            }
        }

        public async Task<string> GetPedidoAsync(int pedidoId, string endpoint)
        {
            var apiUrl = $"{BaseUrl}{endpoint}?id={pedidoId}";
            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                return response;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Erro ao fazer a requisição HTTP: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                throw new Exception($"Erro ao desserializar a resposta JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro desconhecido: {ex.Message}");
            }
        }

        public async Task<bool> ExcluirVendaAsync(int vendaId, string endpoint)
        {
            var apiUrl = $"{BaseUrl}{endpoint}?id={vendaId}";
            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Erro ao fazer a requisição HTTP: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Erro desconhecido: {e.Message}");
            }
        }

        public async Task<bool> InserirVendaAsync(VendaModelSerialized venda, string endpoint)
        {
            var apiUrl = $"{BaseUrl}{endpoint}";
            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var response = await _httpClient.PostAsJsonAsync(apiUrl, venda, jsonOptions);
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(
                        $"Erro ao inserir venda. StatusCode: {response.StatusCode}, Response: {responseContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Erro ao fazer a requisição HTTP: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro desconhecido: {ex.Message}");
            }
        }

        public async Task<UsuarioModel?> LoginAsync(string email, string senha)
        {
            var apiUrl = $"{BaseUrl}/Autenticacao/Login";
            var loginData = new { Email = email, Senha = senha };
            try
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, loginData);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var usuario = JsonSerializer.Deserialize<UsuarioModel>(jsonResponse);
                    return usuario;
                }

                return null;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Erro ao fazer a requisição HTTP: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro desconhecido: {ex.Message}");
            }
        }

        public async Task<bool> CadastrarClienteAsync(ClienteModel clienteModel)
        {
            var apiUrl = $"{BaseUrl}/Cliente/InserirCliente";
            var content = new StringContent(JsonSerializer.Serialize(clienteModel), Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<EnderecoModel?> BuscarEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/Cep/ConsultarCep?cep={cep}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var endereco = JsonSerializer.Deserialize<EnderecoModel>(jsonResponse);
                return endereco;
            }

            return null;
        }
    }
}
