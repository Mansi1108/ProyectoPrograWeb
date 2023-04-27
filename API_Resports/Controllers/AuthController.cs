using API_Resports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using API_Resports.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Proyecto2_Web_SophiaSiguere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ResportsContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ResportsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public async Task<UserToken?> Login(UserAuth userCreds)
        {
            var user = await _context.Usuarios
                            .Where(u => u.NombreUsuario == userCreds.User)
                            .FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            if (user.Contrasena == userCreds.Password)
            {
                return new UserToken
                {
                    Id = user.Id,
                    Username = user.NombreUsuario,
                    Token = CustomTokenJWT(user.NombreUsuario)
                };
            }
            return null;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<UserToken>> Register(reSportsModel.Register usuarionuevo)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'reSportsContext.usuarioNuevo'  is null.");
            }
            Usuario usuario1 = new Usuario
            {
                NombreUsuario = usuarionuevo.NombreUsuario,
                Contrasena = usuarionuevo.Contrasena,
                NombreCompleto = usuarionuevo.NombreCompleto,
                Email = usuarionuevo.Email,
                Genero = "0",
                Edad = usuarionuevo.Edad,
                Experiencia = "",
                Posicion = "",
                Rol = 2,
                EquipoId = 1,
            };

            _context.Usuarios.Add(usuario1);
            await _context.SaveChangesAsync();

            return new UserToken
            {
                Id = usuario1.Id,
                Username = usuarionuevo.NombreUsuario,
                Token = CustomTokenJWT(usuarionuevo.NombreUsuario)
            };
        }

        private string CustomTokenJWT(string username)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)
            );
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
            );
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, username)
            };
            var _Payload = new JwtPayload(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(2)
            );
            var _Token = new JwtSecurityToken(_Header, _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
