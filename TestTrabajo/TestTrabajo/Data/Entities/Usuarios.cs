using System;
using System.ComponentModel.DataAnnotations;

namespace TestTrabajo.Data.Entities
{
    public class Usuarios
    {
        [Key]
        public int usuId { get; set; }
        public string usuSesion { get; set; }
        public string usuContrasena { get; set; }
        public string usuNombre { get; set; }
        public int empId { get; set; }
    }

}
