using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Contrato
    {
        public long Id { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection <Funcionario> Funcionarios { get; set; }
    }
}
