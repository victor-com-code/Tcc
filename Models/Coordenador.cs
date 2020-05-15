using System;
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


        [Display (Name ="Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Nome Usuário")]
        public string NomeDoUsuario { get; set; }

        [RegularExpression(".+@.+\\..+", ErrorMessage = "O campo {0} e-mail é inválido")]
        public string Email { get; set; }

        public string Sexo { get; set; }
        [Display(Name = "Tipo Acesso")]
        public string TipoDeAcesso { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
