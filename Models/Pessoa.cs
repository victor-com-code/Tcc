using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc_Senai.Models
{
    public class Pessoa
    {   [Key]
        public long? IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        
    }
}

