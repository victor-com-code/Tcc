using Microsoft.EntityFrameworkCore;
using Tcc_Senai.Models;

namespace Tcc_Senai.Data
{
    public class IESContext : DbContext
    {
        
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {
        }

        public DbSet<Professor> Professores { get; set; }
        public DbSet<Modalidade> Modalidades { get; set; }
        public DbSet<Coordenador> Coordenadores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<GerarCurso> GerarCursos { get; set; }
        public DbSet<Pedagogo> Pedagogos { get; set; }
        public DbSet<Semestre> Semestres { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<UnidadeCurricular> UnidadeCurriculares { get; set; }
        public DbSet<ProfessorCurso> ProfessorCursos { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessorCurso>().HasKey(sc => new { sc.IdProfessor, sc.IdCurso });


        }


    }
}
