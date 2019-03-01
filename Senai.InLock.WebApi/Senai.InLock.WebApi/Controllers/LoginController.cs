using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Repositories;

namespace Senai.InLock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository UsuarioRepository { get; set; }

        public LoginController()
        {
            UsuarioRepository = new UsuarioRepository();
        }
        [HttpPost]
        public IActionResult Post(UsuarioDomain usuario)
        {
            try
            {
                usuario = UsuarioRepository.BuscarPorEmailSenha(usuario.Email, usuario.Senha);
                if (usuario == null)
                {
                    return NotFound(new
                    {
                        mensagem = "Usuario não encontrado"
                    }
                        );
                }
                //SENTA QUE LÁ VEM MERDA 2.0
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Role, usuario.TipoUsuario)
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("InLockGames-authenticacao"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                       //Nome do Issuer, de onde esta vindo
                       issuer: "InLockGames.WebApi",
                     //Nome da Audience, de onde está vindo
                     audience: "InLockGames.WebApi",
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(30),
                     signingCredentials: creds

                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        }
}