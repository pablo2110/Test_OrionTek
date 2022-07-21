using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTrabajo.Data;
using TestTrabajo.Data.Entities;
using TestTrabajo.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestTrabajo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : BaseController
    {
        private readonly ApiContext _db;

        public ClientesController(ApiContext context)
        {
            _db = context;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] Clientes model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.cliNombre))
                    throw new Exception("El nombre es necesario");

                model.cliFechaCreacion = DateTime.Now;
                model.empId = EmpId;

                model.cliCodigo = Helpers.Utils.GenerateCode(5);

                while (_db.Clientes.Any(r => r.cliCodigo == model.cliCodigo))
                {
                    model.cliCodigo = Helpers.Utils.GenerateCode(5);
                }

                _db.Clientes.Add(model);
                await _db.SaveChangesAsync();

                return Ok($"Cliente {model.cliNombre} agregado correctamente");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesViewModel>>> Get()
        {
            try
            {
                var clientes = from c in _db.Clientes.Where(x => x.empId == EmpId)
                               select new ClientesViewModel()
                               {
                                   cliId = c.cliId,
                                   cliNombre = c.cliNombre,
                                   cliApellidos = c.cliApellidos,
                                   cliCodigo = c.cliCodigo,
                                   empId = c.empId,
                                   cliDirecciones = _db.clienteDirecciones.Where(x => x.cliId == c.cliId).ToList(),
                               };
                return Ok(await clientes.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("Edit")]
        public async Task<ActionResult<string>> Edit([FromBody] Clientes model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.cliNombre))
                    throw new Exception("El nombre es necesario");

                var cli = _db.Clientes.SingleOrDefault(c=>c.cliId == model.cliId);

                if (cli == null)
                    throw new Exception("Cliente no encontrado");

                cli.cliNombre = model.cliNombre;
                cli.cliApellidos = model.cliApellidos = model.cliApellidos;

                _db.Clientes.Update(cli);
                await _db.SaveChangesAsync();

                return Ok($"Cliente {model.cliNombre} Actualizado correctamente");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

        [HttpPost("PostDirreccion")]
        public async Task<ActionResult<string>> PostDireccion([FromBody] ClienteDirecciones model)
        {
            try
            {
                if (model.cliId == 0 || model.cliId ==null)
                    throw new Exception("Id del cliente es necesario");

                if (string.IsNullOrEmpty(model.cliDirDireccion) ||
                    string.IsNullOrEmpty(model.cliDirCiudad) ||
                    string.IsNullOrEmpty(model.cliDirPais))
                    throw new Exception("Dirreccion incorrecta");

                if(_db.clienteDirecciones.Any(cd=>cd.cliDirId == model.cliDirId))
                {
                    _db.clienteDirecciones.Update(model);
                }
                else
                {
                    _db.clienteDirecciones.Add(model);
                }
                await _db.SaveChangesAsync();

                return Ok($"Direccion agregado correctamente");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }
    }
}
