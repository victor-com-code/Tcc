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

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        [MaxLength(30)]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O campo Contrato é obrigatório.")]
        [Display(Name = "Contrato")]
        public string TipoDeContrato { get; set; }

        [Required(ErrorMessage = "O campo Horário de Trabalho é obrigatório.")]
        [Display(Name = "Horário de Trabalho")]
        [Range(1, 10, ErrorMessage = "A jornada de trabalho diária não pode ultrapassar 10 horas.")]
        public int HorarioDeTrabalho { get; set; }

        [Required(ErrorMessage = "O campo Carga Horária Semanal é obrigatório.")]
        [Display(Name = "Carga Horária Semanal")]
        [Range(1, 70, ErrorMessage = "A jornada de trabalho semanal não pode ultrapassar 70 horas.")]
        public int CargaHorariaSemanal { get; set; }

        public virtual ICollection<ProfessorCurso> ProfessorCursos { get; set; }

    }
}
