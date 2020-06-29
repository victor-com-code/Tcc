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
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Unidade Curricular é obrigatório.")]
        [Display (Name = "Unidade Curricular")]
        public string Nome{ get; set;}
        public List<CursoUnidadeCurricular> CursoUnidadeCurriculares { get; set; }

        public virtual ICollection<Aula> Aulas { get; set; }

    }
}
