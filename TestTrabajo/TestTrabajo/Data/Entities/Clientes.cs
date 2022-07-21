using System;
using System.ComponentModel.DataAnnotations;

namespace TestTrabajo.Data.Entities
{
    public class Clientes
    {
        [Key]
        public int cliId { get; set; }
        public string cliNombre { get; set; }
        public string cliApellidos { get; set; }
        public string cliCodigo { get; set; }
        public int empId { get; set; }
        public DateTime cliFechaCreacion { get; set; }
    }
}
