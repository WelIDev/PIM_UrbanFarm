using Dominio.Entidades;
using Dominio.Interfaces;
using Newtonsoft.Json;

namespace Infraestrutura.Servicos;

public class CepServico : ICepServico
{
    private readonly HttpClient _httpClient;

    public CepServico(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Endereco ConsultarCep(string cep)
    {
        var response = _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/").Result;
        try
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erro ao consultar o CEP: " + response.StatusCode);
            }

            var resultado = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Endereco>(resultado) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro inesperado: " + e.Message);
            throw;
        }
    }
}