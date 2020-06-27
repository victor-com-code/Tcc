using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc_Senai.Models
{
    public class Funcionario
    {
        [Key]
        [Display(Name = "Código")]
        public long Id { get; set; }

        [ForeignKey("Perfil")]
        [Display(Name = "Perfil")]
        public long IdPerfil { get; set; }
        public virtual Perfil Perfil { get; set; }

        [ForeignKey("Contrato")]
        [Display(Name = "Perfil")]
        public long IdContrato { get; set; }
        public virtual Contrato Contrato { get; set; }

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        [Display (Name ="Nome Completo")]
        [MaxLength(50)]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [Display(Name = "E-mail")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "O campo {0} é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(20, ErrorMessage = "A senha deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório.")]
        [Compare("Senha", ErrorMessage = "Senha e Confirmar Senha não são as mesmas.")]
        [MaxLength(20)]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "O campo Horário de Trabalho é obrigatório.")]
        [Display(Name = "Horário de Trabalho")]
        [Range(1, 10, ErrorMessage = "A jornada de trabalho diária não pode ultrapassar 10 horas.")]
        public int Horario { get; set; }

        [Required(ErrorMessage = "O campo Carga Horária Semanal é obrigatório.")]
        [Display(Name = "Carga Horária Semanal")]
        [Range(1, 70, ErrorMessage = "A jornada de trabalho semanal não pode ultrapassar 70 horas.")]
        public int CargaHorariaSemanal { get; set; }

        public List<FuncionarioCurso> FuncionarioCursos { get; set; }

    }
}
