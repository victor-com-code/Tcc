using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Turma
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Período é obrigatório.")]
        [Display(Name = "Período")]
        public string Periodo { get; set; }

        public string Sigla { get; set; }

        [Display (Name= "Curso")]
        [ForeignKey("Curso")]
        public long IdCurso { get; set; }
        public virtual Curso Curso { get; set; }

    }
}
