using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Perfil
    {
        [Key]
        public long Id { get; set; }
        public string Nivel { get; set; }

        public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }
}
