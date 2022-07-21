using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestTrabajo.Data.Entities;

namespace TestTrabajo.ViewModel
{
    public class ClientesViewModel
    {
        [Key]
        public int cliId { get; set; }
        public string cliNombre { get; set; }
        public string cliApellidos { get; set; }
        public string cliCodigo { get; set; }
        public int empId { get; set; }
        public DateTime cliFechaCreacion { get; set; }
        public Clientes cliente;
        public ICollection<ClienteDirecciones> cliDirecciones { get; set; }
    }
}
