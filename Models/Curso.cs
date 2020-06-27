using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc_Senai.Models
{
    public class Curso
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Curso é obrigatório.")]
        [Display(Name = "Curso")]
        public string Nome { get; set; }

        [ForeignKey("Modalidade")]
        [Display(Name = "Modalidade")]
        public long IdMod { get; set; }
        public Modalidade Modalidade { get; set; }       

        [Required(ErrorMessage = "O campo Carga Horária é obrigatório.")]
        [Display(Name = "Carga Horária")]
        public int? CargaHoraria { get; set; }

        [Required(ErrorMessage = "O campo Sigla é obrigatório.")]
        [MaxLength(3)]
        public string Sigla { get; set; }

        public List<CursoUnidadeCurricular> CursoUnidadeCurriculares { get; set; }
        public List<FuncionarioCurso> FuncionarioCursos { get; set; }
        public virtual ICollection<Turma> Turmas { get; set; }
       

    }
}

