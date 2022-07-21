using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestTrabajo.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        public int UsuId { get; set; }
        public int EmpId { get; set; }
        public string UsuSesion { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //Validar si el Metodo tiene el attribute AllowAnonymous
            if (context.Filters.Any(x => x.GetType() == typeof(Microsoft.AspNetCore.Mvc.Authorization.AllowAnonymousFilter)))
                return;


            ClaimsIdentity claims = (ClaimsIdentity)User.Identity;
            if (claims.Claims.Count() > 0)
            {
                UsuSesion = claims.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                UsuId = Convert.ToInt32(claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                EmpId = Convert.ToInt32(claims.Claims.First(c => c.Type == ClaimTypes.PrimarySid).Value);
            }

        }
    }
}
