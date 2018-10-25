using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xpto.Data
{
    public class Clientes : IEntities
    {
        public Clientes()
        {
            this.Produtos = new HashSet<Produtos>();
        }

        [Key]
        public int ClienteId { get; set; }
        public int CodReferencia { get; set; }
        [StringLength(250)]
        public string Nome { get; set; }
        [StringLength(250)]
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        public SexoEnum Sexo { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }
    }
}
