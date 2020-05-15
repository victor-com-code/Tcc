using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Modalidade
    {
        [Key]
        public long? IdModalidade { get; set; }

        [Display(Name = "Modalidade")]
        public string NomeModalidade { get; set; }

        
      public virtual ICollection<Curso> Cursos { get; set; }
        
      public virtual ICollection<UnidadeCurricular> UnidadeCurriculars { get; set; }
    }
}
