﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Coordenador
    {
        [Key]
        public long? IdCoordenador { get; set; }

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        [Display (Name ="Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo Nome Usuário é obrigatório.")]
        [Display(Name = "Nome Usuário")]
        public string NomeDoUsuario { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [Display(Name = "E-mail")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "O campo {0} e-mail é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "O Tipo Acesso é obrigatório.")]
        [Display(Name = "Tipo Acesso")]
        public string TipoDeAcesso { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório!")]
        [Compare("Senha", ErrorMessage = "Senha e confirmação não são as mesmas.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmarSenha { get; set; }
    }
}
