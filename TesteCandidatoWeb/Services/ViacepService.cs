using Microsoft.CSharp.RuntimeBinder;
using System.Text.Json.Nodes;
using TesteCandidatoWeb.Models;

namespace TesteCandidatoWeb.Services;

public class ViacepService
{
    private HttpClient HttpClient { get; }

    private const string _viaCepBaseUrl = "https://viacep.com.br/ws";
    private const string _returnFormat = "json";

    public ViacepService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    private static string BuildFullUrl(string cep)
    {
        return $"{_viaCepBaseUrl}/{cep}/{_returnFormat}";
    }

    public async Task<Endereco?> GetByCep(string cep)
    {
        Endereco? cepModel = null;
        var url = BuildFullUrl(cep);

        using var response = await HttpClient.GetAsync(url);

        if (response.IsSuccessStatusCode && !await HasError(response.Content))
            cepModel = await response.Content.ReadFromJsonAsync<Endereco>();

        return cepModel;
    }

    public async Task<bool> HasError(HttpContent content)
    {
        var json = JsonNode.Parse(await content.ReadAsStringAsync());

        try
        {
            return json?["erro"]?.GetValue<bool>() ?? false;
        }
        catch (Exception ex) when (ex is FormatException or InvalidOperationException or RuntimeBinderException)
        {
            return false;
        }
    }
}
