using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PetLog.Aplicacao;

namespace PetLog.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AIService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GerarRespostaAsync(string prompt)
        {
            var url = _config["Groq:ApiUrl"];
            var chave = _config["Groq:ApiKey"];
            var modelo = _config["Groq:Model"];

            var requisicao = new HttpRequestMessage(HttpMethod.Post, url);
            requisicao.Headers.Authorization = new AuthenticationHeaderValue("Bearer", chave);

            var corpo = new
            {
                model = modelo,
                temperature = 0.3,
                max_tokens = 400,
                messages = new[] { new { role = "user", content = prompt } }
            };

            requisicao.Content = new StringContent(
                JsonSerializer.Serialize(corpo), Encoding.UTF8, "application/json");

            var resposta = await _httpClient.SendAsync(requisicao);
            var texto = await resposta.Content.ReadAsStringAsync();

            if (!resposta.IsSuccessStatusCode)
                throw new HttpRequestException($"Erro da IA ({(int)resposta.StatusCode}): {texto}");

            using var json = JsonDocument.Parse(texto);

            return json.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;
        }
    }
}