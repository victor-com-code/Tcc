using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Semestre
    {
        [Key]
        public long? IdSemestre { get; set; }
        
        [Display (Name = "Semestre")]
        public string NomeSemestre { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat (DataFormatString = "{0:yyyy}")]
        public DateTime Ano { get; set; }

    }
}
