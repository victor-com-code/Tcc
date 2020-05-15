using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tcc_Senai.Models
{
    public class Professor
    {
        [Key]
        [Display(Name = "Código")] 
        public long? IdProfessor { get; set; }
        [Required]
        [StringLength(50)
            ]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Nome")]
        public string PrimeiroNome { get; set; }

        [Display(Name = "Contrato")]
        public string TipoDeContrato { get; set; }

        [Display(Name = "Horário de Trabalho")]
        public string HorarioDeTrabalho { get; set; }

        [Display(Name = "Carga Horária")]
        public string CargaHorariaSemanal { get; set; }

        public virtual ICollection<ProfessorCurso> ProfessorCursos { get; set; }

    }
}
