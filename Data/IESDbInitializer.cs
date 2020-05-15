using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc_Senai.Models;

namespace Tcc_Senai.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Modalidades.Any())
            {
                return;
            }
            var modalidades = new Modalidade[]
            {
                new Modalidade {NomeModalidade="Técnico"},
                new Modalidade {NomeModalidade="Profissional"},
                new Modalidade {NomeModalidade="Aperfeiçoamento"},
                new Modalidade {NomeModalidade="Avançado"}
            };
            foreach (Modalidade d in modalidades)
            {
                context.Modalidades.Add(d);
            }

            context.SaveChanges();
        }
    }
}
    


