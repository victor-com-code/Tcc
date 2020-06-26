using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class FuncionarioCurso
    {
        public int IdCurso { get; set; }
        public Curso Curso { get; set; }
        public int IdFunc { get; set; }
        public Funcionario Funcionario { get; set; }

    }
}
