using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class CursoUnidadeCurricular
    {
        public long IdCurso { get; set; }
        public Curso Curso { get; set; }
        public long IdUc { get; set; }
        public UnidadeCurricular UnidadeCurricular { get; set; }
    }
}
