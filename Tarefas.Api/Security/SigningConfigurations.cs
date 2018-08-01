using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Tarefas.Api.Security
{
    public class SigningConfigurations
    {
        private const string SECRET_KEY = "aeg6esv7-6138-db66-bdrsd64sdfe6";

        public SigningCredentials signingCredentials { get; }
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public SigningConfigurations()
        {
            signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
