using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc_Senai.Models
{
    public class Curso
    {
        [Key]
        public long? IdCurso { get; set; }

        [Required]
        [Display(Name = "Curso")]
        public string NomeCurso { get; set; }

        [ForeignKey("Modalidade")]
        [Display(Name = "Modalidade")]
        public long? IdModalidade { get; set; }
        public virtual  Modalidade Modalidade { get; set; }

        [ForeignKey("UnidadeCurricular")]
        [Display(Name = "Unidade Curricular")]
        public long? IdUc { get; set; }
        public virtual UnidadeCurricular UnidadeCurricular { get; set; }

        [Required]
        [Display(Name = "Carga Horária")]
        public string CargaHoraria { get; set; }

        [Required]
        public string Sigla { get; set; }


        //public virtual ICollection<UnidadeCurricular> UnidadeCurricular { get; set; }

        //public virtual ICollection<ProfessorCurso> ProfessorCursos { get; set;} 
        public virtual ICollection<Turma> Turmas { get; set; }

    }
}

