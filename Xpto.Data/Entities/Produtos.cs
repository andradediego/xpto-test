using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xpto.Data
{
    public class Produtos : IEntities
    {
        public Produtos()
        {
            this.Clientes = new HashSet<Clientes>();
        }

        [Key]
        public int ProdutoId { get; set; }
        public int CodReferencia { get; set; }
        [StringLength(250)]
        public string Nome { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
