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
                new Modalidade {Nome="Técnico"},
                new Modalidade {Nome="Profissional"},
                new Modalidade {Nome="Aperfeiçoamento"},
                new Modalidade {Nome="Avançado"}
            };
            foreach (Modalidade d in modalidades)
            {
                context.Modalidades.Add(d);
            }

            context.SaveChanges();
        }
    }
}
    


