using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestTrabajo.Data;
using TestTrabajo.Data.Entities;
using TestTrabajo.Helpers;
using TestTrabajo.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestTrabajo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : BaseController
    {
        private readonly ApiContext _db;
        public UsuariosController(ApiContext context)
        {
            _db = context;
        }

        [AllowAnonymous]
        [HttpPost("/Login")]
        public ActionResult Login([FromBody] LoginViewModel args)
        {
            var usuario = _db.Usuarios.SingleOrDefault(x => (x.usuSesion.Equals(args.Usuario) &&
            x.usuContrasena.Equals(Encryption.Encrypt(args.Password))));

            if (usuario == null) return BadRequest("Nombre de usuario o contraseña incorrectos!.");

            var res = new LoginResult()
            {
                usuId = usuario.usuId,
                usuNombre = usuario.usuNombre,
                empId = usuario.empId,
                usuSesion = usuario.usuSesion,
                Token = GenerateToken(usuario)
            };

            return Ok(res);
        }

        private string GenerateToken(Usuarios args)
        {
            //Generar Token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constantes.PASS_PHRASE);
            var tokenExpires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, args.usuSesion),
                    new Claim(ClaimTypes.NameIdentifier, args.usuId.ToString()),
                    new Claim(ClaimTypes.PrimarySid, args.empId.ToString())
                }),
                Expires = tokenExpires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
