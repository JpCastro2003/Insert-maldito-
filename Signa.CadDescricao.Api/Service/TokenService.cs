using Microsoft.IdentityModel.Tokens;
using Signa.Library.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;

namespace Signa.CadDescricao.Api.Service
{
    public class TokenService
    {
        public static string CreateToken()
        {
            var identity = new ClaimsIdentity(
            new[] {
            new Claim("UserId", Global.UsuarioId.ToString()),
            new Claim("FuncaoId", Global.FuncaoId.ToString()),
            new Claim("GrupoUsuarioId", Global.GrupoUsuarioId.ToString()),
            }
            );



            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromMinutes(Global.TokenMinutes);



            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao,
                SigningCredentials = Global.SigningCredentials
            });



            return handler.WriteToken(securityToken);
        }
    }
}
