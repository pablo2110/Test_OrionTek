using System;
using System.ComponentModel.DataAnnotations;

namespace TestTrabajo.Data.Entities
{
    public class Empresa
    {
        [Key]
        public int empId { get; set; }
        public string empNombre { get; set; }
        public string empDireccion { get; set; }
        public string empTelefono { get; set; }
        public string empLogoPatch { get; set; }
    }
}
