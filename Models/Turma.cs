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

        [Display(Name= "Curso")]
        [ForeignKey("Curso")]
        public long IdCurso { get; set; }
        public virtual Curso Curso { get; set; }

        [Required(ErrorMessage = "O campo Módulo é obrigatório.")]
        [Display(Name = "Módulo")]
        public int? Modulo { get; set; }

        [Required(ErrorMessage = "O campo Sigla é obrigatório.")]
        [MaxLength(4)]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "O campo Ano é obrigatório.")]
        public int? Ano { get; set; }

        [Required(ErrorMessage = "O campo Semestre é obrigatório.")]
        [Display(Name = "Semestre")]
        public string Semestre { get; set; }

        public virtual ICollection<Aula> Aulas { get; set; }
    }
}
