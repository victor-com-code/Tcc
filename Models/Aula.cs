using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Aula
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Turma")]
        [ForeignKey("Turma")]
        public long IdTurma { get; set; }
        public Turma Turma { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Display(Name = "Horário Início")]
        [DataType(DataType.Time)]
        public DateTime HorarioInicio { get; set; }

        [Display(Name = "Horário Fim")]
        [DataType(DataType.Time)]
        public DateTime HorarioFim { get; set; }

        [Display(Name = "Unidade Curricular")]
        [ForeignKey("UnidadeCurricular")]
        public long IdUc { get; set; }
        public UnidadeCurricular UnidadeCurricular { get; set; }

        [Display(Name = "Funcionário")]
        [ForeignKey("Funcionario")]
        public long IdFunc { get; set; }
        public Funcionario Funcionario { get; set; }

    }
}
