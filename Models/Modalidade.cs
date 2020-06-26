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
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Modalidade é obrigatório.")]
        [Display(Name = "Modalidade")]
        public string Nome { get; set; }
        public virtual ICollection<Curso> Cursos { get; set; }
       
    }
}
