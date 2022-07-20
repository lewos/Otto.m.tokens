using Otto.m.tokens.DTOs;
using Otto.m.tokens.Models;

namespace Otto.m.tokens.Mapper
{
    public static class MTokenMapper
    {
        public static MToken GetMToken(MTokenDTO dto) 
        {
            return new MToken {
                ExpiresAt = dto.ExpiresAt,
                AccessToken = dto.AccessToken,
                Active = dto.Active,
                BusinessId = dto.BusinessId,
                Created = dto.Created,
                Id = dto.Id,
                Modified= dto.Modified,
                MUserId = dto.MUserId,
                RefreshToken= dto.RefreshToken,
                Type = dto.Type,
                UserId = dto.UserId            
            };
        }


        public static MTokenDTO GetMTokenDTO(MToken token)
        {
            return new MTokenDTO
            {
                ExpiresAt = token.ExpiresAt,
                AccessToken = token.AccessToken,
                Active = token.Active,
                BusinessId = token.BusinessId,
                Created = token.Created,
                Id = token.Id,
                Modified = token.Modified,
                MUserId = token.MUserId,
                RefreshToken = token.RefreshToken,
                Type = token.Type,
                UserId = token.UserId
            };
        }
    }
}
