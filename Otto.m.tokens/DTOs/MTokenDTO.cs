using System.Text.Json.Serialization;

namespace Otto.m.tokens.DTOs
{
    public class MTokenDTO
    {
        public long Id { get; set; }
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
        [JsonPropertyName("business_id")]
        public long? BusinessId { get; set; }
        [JsonPropertyName("m_user_id")]
        public long? MUserId { get; set; }
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = null!;
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = null!;
        [JsonPropertyName("token_type")]
        public string? Type { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? Active { get; set; }
        [JsonPropertyName("expires_at")]
        public DateTime? ExpiresAt { get; set; }
        [JsonPropertyName("expires_in")]
        public long? ExpiresIn { get; set; }
    }
}
