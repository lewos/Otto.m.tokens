using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otto.m.tokens.Models
{
    public partial class MToken
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public long? BusinessId { get; set; }
        public long? MUserId { get; set; }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string? Type { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? Active { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
