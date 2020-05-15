using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class ProfessorCurso
    {
        [ForeignKey("Curso")]
        public long? IdCurso { get; set;}
        public virtual Curso Curso { get; set; }

        [Column(Order = 1)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(2, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [ForeignKey("Professor")]
        public long? IdProfessor { get; set; }
        public virtual Professor Professor { get; set; } 

    }
}
