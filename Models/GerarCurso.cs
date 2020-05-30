using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class GerarCurso
    {
        [Key]
        public long? IdGerarCursos { get; set; }

        [Required(ErrorMessage = "O campo Início do Curso é obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Início do Curso")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O campo Fim do Curso é obrigatório.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fim do Curso")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O campo Modalidade é obrigatório.")]
        [Display(Name = "Modalidade")]
        public string NomeModalidade { get; set; }

        [Required(ErrorMessage = "O campo Curso é obrigatório.")]
        [Display(Name = "Curso")]
        public string NomeCurso { get; set; }
        
        public virtual ICollection<Curso> Cursos { get; set; }
        public virtual ICollection<Modalidade> Modalidades { get; set; }
        public virtual ICollection<UnidadeCurricular> UnidadeCurriculars { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }
    }
}
