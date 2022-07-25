using Otto.m.tokens.DTOs;

namespace Otto.m.tokens.Models
{
    public class MRefreshTokenResponse
    {
        public MRefreshTokenResponse(Response r, string v, MRefreshTokenDTO mToken)
        {
            res = r;
            msg = v;
            token = mToken;
        }

        public Response res { get; set; }

        public string msg { get; set; }

        public MRefreshTokenDTO token { get; set; }
    }
}
