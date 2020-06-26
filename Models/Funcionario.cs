using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tcc_Senai.Models
{
    public class Funcionario
    {
        [Key]
        [Display(Name = "Código")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        [MaxLength(30)]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }

        //[Required(ErrorMessage = "O campo Contrato é obrigatório.")]
        //[Display(Name = "Contrato")]
        //public string TipoDeContrato { get; set; }

        [Display(Name = "Horário de Trabalho")]
        [Range(1, 10, ErrorMessage = "A jornada de trabalho diária não pode ultrapassar 10 horas.")]
        public int Horario { get; set; }

        [Display(Name = "Carga Horária Semanal")]
        [Range(1, 70, ErrorMessage = "A jornada de trabalho semanal não pode ultrapassar 70 horas.")]
        public int CargaHorariaSemanal { get; set; }

        public List<FuncionarioCurso> FuncionarioCursos { get; set; }

        // Falta Perfil

        //falta contrato
    }
}
