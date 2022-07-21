using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using TestTrabajo.Data.Entities;

namespace TestTrabajo.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
{
}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<ClienteDirecciones> clienteDirecciones { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
