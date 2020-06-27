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

            //populando Modalidades
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

            //populando perfis
            if (context.Perfis.Any())
            {
                return;
            }
            var perfis = new Perfil[]
            {
                new Perfil {Nivel="Professor"},
                new Perfil {Nivel="Supervisor"},
                new Perfil {Nivel="Pedagogo"}
            };
            foreach (Perfil p in perfis)
            {
                context.Perfis.Add(p);
            }


            if (context.Contratos.Any())
            {
                return;
            }
            var contratos = new Contrato[]
            {
                new Contrato {Tipo="Intermitente"},
                new Contrato {Tipo="Mensalista"},
                new Contrato {Tipo="Horista"},
                new Contrato {Tipo="RPA"}
            };
            foreach (Contrato c in contratos)
            {
                context.Contratos.Add(c);
            }

            context.SaveChanges();
        }
    }
}
    


