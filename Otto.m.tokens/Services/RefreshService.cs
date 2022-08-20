using Microsoft.Net.Http.Headers;
using Otto.m.tokens.DTOs;
using Otto.m.tokens.Models;
using System.Text.Json;

namespace Otto.m.tokens.Services
{
    public class RefreshService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string? _clientId;
        private readonly string? _clientSecret;

        public RefreshService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _clientId = Environment.GetEnvironmentVariable("APP_ID");
            _clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        }

        public async Task<MRefreshTokenResponse> RefreshToken(long MUserId, string RefreshToken)
        {
            try
            {
                string baseUrl = "https://api.mercadolibre.com";
                string endpoint = "oauth/token";
                string url = string.Join('/', baseUrl, endpoint);

                var data = new Dictionary<string, string>
                {
                    {"grant_type", "refresh_token"},
                    {"client_id", _clientId},
                    {"client_secret", _clientSecret},
                    {"refresh_token", RefreshToken}
                    
                };


                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.PostAsync(url, new FormUrlEncodedContent(data));

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();


                    var mToken = await JsonSerializer.DeserializeAsync
                        <MRefreshTokenDTO>(contentStream);

                    return new MRefreshTokenResponse(Response.OK, $"{Response.OK}", mToken);

                }

                return new MRefreshTokenResponse(Response.WARNING, $"No se obtuvo el refresh token del usuario {MUserId}", null);

            }
            catch (Exception ex)
            {
                return new MRefreshTokenResponse(Response.ERROR, $"Error al obtener el refresh token del usuario {MUserId}. Ex : {ex}", null);

            }


        }


    }
}
