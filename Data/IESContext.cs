using Microsoft.EntityFrameworkCore;
using Tcc_Senai.Models;

namespace Tcc_Senai.Data
{
    public class IESContext : DbContext
    {
        
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Modalidade> Modalidades { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoUnidadeCurricular> CursoUnidadeCurriculares { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<UnidadeCurricular> UnidadeCurriculares { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<FuncionarioCurso> FuncionarioCursos { get; set; }

        //public DbSet<GerarCurso> GerarCursos { get; set; }
        //public DbSet<Pedagogo> Pedagogos { get; set; }
        //public DbSet<Semestre> Semestres { get; set; }
        //public DbSet<Coordenador> Coordenadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CursoUnidadeCurricular>().HasKey(sc => new { sc.IdCurso, sc.IdUc });

            modelBuilder.Entity<FuncionarioCurso>().HasKey(sc => new { sc.IdCurso, sc.IdFunc });
        }


    }
}
