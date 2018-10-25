using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Xpto.Data
{
    public class XptoDbContext : DbContext
    {
        public XptoDbContext() : base("XptoDbContext")
        {
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Produtos> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
