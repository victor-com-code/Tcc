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

            //Populando Modalidades
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
            foreach (Modalidade m in modalidades)
            {
                context.Modalidades.Add(m);
            }

            //Populando Perfis
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

            //Populando Contratos
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

            //Populando Unidades Curriculares
            if (context.UnidadeCurriculares.Any())
            {
                return;
            }
            var unidadesCurriculares = new UnidadeCurricular[]
            {
                new UnidadeCurricular {Nome="Lógica de Programação"},
                new UnidadeCurricular {Nome="Informática Aplicada"},
                new UnidadeCurricular {Nome="Fundamentos da Tecnologia da Informação"},
                new UnidadeCurricular {Nome="Comunicação oral e escrita"}
            };
            foreach (UnidadeCurricular u in unidadesCurriculares)
            {
                context.UnidadeCurriculares.Add(u);
            }

            context.SaveChanges();
        }
    }
}
    


