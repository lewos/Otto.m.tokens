using System.Text.Json.Serialization;

namespace Otto.m.tokens.DTOs
{
    public class MRefreshTokenDTO
    {

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string Type { get; set; }
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("user_id")]
        public long MUserId { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

    }
}
