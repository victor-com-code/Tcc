using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class UnidadeCurricular
    {
        [Key]
        public long? IdUc { get; set; }

        [Required(ErrorMessage = "O campo Unidade Curricular é obrigatório.")]
        [Display (Name = "Unidade Curricular")]
        public string NomeUnidadeCurricular{ get; set;}


        [ForeignKey("Curso")]
        public long? IdCurso { get; set; }
        public virtual Curso Curso { get; set; }

        [ForeignKey("Professor")]
        public long? IdProfessor { get; set; }
        public virtual Professor Professor { get; set; }

    }
}
