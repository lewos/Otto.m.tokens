using Microsoft.EntityFrameworkCore;
using Otto.m.tokens.DTOs;
using Otto.m.tokens.Mapper;
using Otto.m.tokens.Models;

namespace Otto.m.tokens.Services
{
    public class MTokenService
    {

        private readonly OttoContext _context;
        private readonly RefreshService _refreshService;

        public MTokenService(OttoContext context, RefreshService refreshService)
        {
            _context = context;
            _refreshService = refreshService;
        }

        public async Task<List<MTokenDTO>> GetAsync()
        {
            var response = new List<MTokenDTO>();
            var tokens = await _context.MTokens.ToListAsync();
            foreach (var token in tokens)
                response.Add(MTokenMapper.GetMTokenDTO(token));

            return response;

        }

        public async Task<MTokenDTO> GetMTokenByUserAsync(long id)
        {

            var token = await _context.MTokens.Where(t => t.MUserId == id).FirstOrDefaultAsync();
            if (token != null)
            {
                return MTokenMapper.GetMTokenDTO(token);
            }

            return null;
        }


        public async Task<MTokenDTO> RefreshMTokenByUserAsync(long id)
        {

            var token = await _context.MTokens.Where(t => t.MUserId == id).FirstOrDefaultAsync();
            if (token != null)
            {
                // Llamar al servicio para hacer el refresh
                var response = await _refreshService.RefreshToken((long)token.MUserId, token.RefreshToken);

                // hacer el update en la base
                if (response.res == Response.OK)
                {
                    UpdateTokenProperties(response.token, token);
                    UpdateDateTimeKindForPostgress(token);

                    _context.Entry(token).State = EntityState.Modified;
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected < 1) 
                    {
                        Console.WriteLine("Hubo un error al hacer el update en la tabla");
                        return null;
                    }

                }
                else 
                {
                    Console.WriteLine($"La respuesta del refresh no fue OK: {response.msg}");
                    return null;
                }

                // devolver el token
                return MTokenMapper.GetMTokenDTO(token);
            }

            Console.WriteLine($"El token de ese usuario no esta en la base {id}");
            return null;
        }


        //MUserId



        /// <summary>
        /// Hace el update del token usando el id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mToken"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> UpdateAsync(long id, MTokenDTO dto)
        {
            // Si ya existe un token con ese mismo usuario, hago el update
            var token = await _context.MTokens.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (token != null)
            {
                UpdateTokenProperties(dto, token);
                UpdateDateTimeKindForPostgress(token);
            }

            _context.Entry(token).State = EntityState.Modified;
            return await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Hace el update del token usando el id de mercadolibre
        /// </summary>
        /// <param name="mToken"></param>
        /// <returns>number of rows affected</returns>
        public async Task<Tuple<MTokenDTO, int>> UpdateWithMTokenIdAsync(MTokenDTO dto)
        {
            // Si ya existe un token con ese mismo usuario, hago el update
            var token = await _context.MTokens.Where(t => t.MUserId == dto.MUserId).FirstOrDefaultAsync();
            if (token != null && token.MUserId == dto.MUserId)
            {
                UpdateTokenProperties(dto, token);
                UpdateDateTimeKindForPostgress(token);
            }


            _context.Entry(token).State = EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();

            var newDTO = MTokenMapper.GetMTokenDTO(token);

            return new Tuple<MTokenDTO, int>(newDTO, rowsAffected);

        }

        private static void UpdateTokenProperties(MTokenDTO dto, MToken? token)
        {
            var utcNow = DateTime.UtcNow;

            token.AccessToken = dto.AccessToken;
            token.RefreshToken = dto.RefreshToken;
            token.Modified = DateTime.UtcNow;
            token.ExpiresAt = utcNow + TimeSpan.FromSeconds((double)dto.ExpiresIn);
        }

        private static void UpdateTokenProperties(MRefreshTokenDTO dto, MToken? token)
        {
            var utcNow = DateTime.UtcNow;

            token.AccessToken = dto.AccessToken;
            token.RefreshToken = dto.RefreshToken;
            token.Modified = DateTime.UtcNow;
            token.ExpiresAt = utcNow + TimeSpan.FromSeconds((double)dto.ExpiresIn);
        }



        private static void UpdateDateTimeKindForPostgress(MToken token) 
        {
            token.Created = DateTime.SpecifyKind((DateTime) token.Created, DateTimeKind.Utc);
            token.Modified = DateTime.SpecifyKind((DateTime) token.Modified, DateTimeKind.Utc);
            token.ExpiresAt = DateTime.SpecifyKind((DateTime) token.ExpiresAt, DateTimeKind.Utc);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mToken"></param>
        /// <returns>ID and number of rows affected</returns>
        public async Task<Tuple<MTokenDTO, int>> Create(MTokenDTO dto)
        {
            var utcNow = DateTime.UtcNow;

            var mToken = MTokenMapper.GetMToken(dto);

            mToken.Created = utcNow;
            mToken.Modified = utcNow;
            mToken.Active = true;
            mToken.ExpiresAt = utcNow + TimeSpan.FromSeconds((double)dto.ExpiresIn);

            _context.MTokens.Add(mToken);
            var rowsAffected = await _context.SaveChangesAsync();


            var newDTO = MTokenMapper.GetMTokenDTO(mToken);

            return new Tuple<MTokenDTO, int>(newDTO, rowsAffected);

        }

    }
}
