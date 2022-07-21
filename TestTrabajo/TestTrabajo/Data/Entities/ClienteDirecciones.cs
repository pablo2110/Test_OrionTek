using System;
using System.ComponentModel.DataAnnotations;

namespace TestTrabajo.Data.Entities
{
    public class ClienteDirecciones
    {
        [Key]
        public int cliDirId { get; set; }
        public int cliId { get; set; }
        public string cliDirDireccion { get; set; }
        public string cliDirSector { get; set; }
        public string cliDirCiudad { get; set; }
        public string cliDirPais { get; set; }
        public bool cliDirPrincipal { get; set; }
    }
}
