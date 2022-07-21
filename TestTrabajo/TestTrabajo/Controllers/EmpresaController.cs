using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTrabajo.Data;
using TestTrabajo.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestTrabajo.Controllers
{
    [Route("api/[controller]")]
    public class EmpresaController : BaseController
    {
        private readonly ApiContext _db;

        public EmpresaController(ApiContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<Empresa>> Get()
        {
            try
            {
                var miEmpresa = _db.Empresa.SingleOrDefault(em=>em.empId == EmpId);

                return Ok(miEmpresa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
