﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tcc_Senai.Data;

namespace Tcc_Senai.Migrations
{
    [DbContext(typeof(IESContext))]
    [Migration("20200628172503_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tcc_Senai.Models.Contrato", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Curso", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CargaHoraria")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<long>("IdMod")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.HasIndex("IdMod");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Tcc_Senai.Models.CursoUnidadeCurricular", b =>
                {
                    b.Property<long>("IdCurso")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUc")
                        .HasColumnType("bigint");

                    b.Property<long?>("CursoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UnidadeCurricularId")
                        .HasColumnType("bigint");

                    b.HasKey("IdCurso", "IdUc");

                    b.HasIndex("CursoId");

                    b.HasIndex("UnidadeCurricularId");

                    b.ToTable("CursoUnidadeCurriculares");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Funcionario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CargaHorariaSemanal")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("ConfirmarSenha")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Horario")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<long>("IdContrato")
                        .HasColumnType("bigint");

                    b.Property<long>("IdPerfil")
                        .HasColumnType("bigint");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("IdContrato");

                    b.HasIndex("IdPerfil");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("Tcc_Senai.Models.FuncionarioCurso", b =>
                {
                    b.Property<int>("IdCurso")
                        .HasColumnType("int");

                    b.Property<int>("IdFunc")
                        .HasColumnType("int");

                    b.Property<long?>("CursoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FuncionarioId")
                        .HasColumnType("bigint");

                    b.HasKey("IdCurso", "IdFunc");

                    b.HasIndex("CursoId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("FuncionarioCursos");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Modalidade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Modalidades");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Perfil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nivel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Perfis");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Turma", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Ano")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<long>("IdCurso")
                        .HasColumnType("bigint");

                    b.Property<int?>("Modulo")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Semestre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.HasKey("Id");

                    b.HasIndex("IdCurso");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("Tcc_Senai.Models.UnidadeCurricular", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UnidadeCurriculares");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Curso", b =>
                {
                    b.HasOne("Tcc_Senai.Models.Modalidade", "Modalidade")
                        .WithMany("Cursos")
                        .HasForeignKey("IdMod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tcc_Senai.Models.CursoUnidadeCurricular", b =>
                {
                    b.HasOne("Tcc_Senai.Models.Curso", "Curso")
                        .WithMany("CursoUnidadeCurriculares")
                        .HasForeignKey("CursoId");

                    b.HasOne("Tcc_Senai.Models.UnidadeCurricular", "UnidadeCurricular")
                        .WithMany("CursoUnidadeCurriculares")
                        .HasForeignKey("UnidadeCurricularId");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Funcionario", b =>
                {
                    b.HasOne("Tcc_Senai.Models.Contrato", "Contrato")
                        .WithMany("Funcionarios")
                        .HasForeignKey("IdContrato")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tcc_Senai.Models.Perfil", "Perfil")
                        .WithMany("Funcionarios")
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tcc_Senai.Models.FuncionarioCurso", b =>
                {
                    b.HasOne("Tcc_Senai.Models.Curso", "Curso")
                        .WithMany("FuncionarioCursos")
                        .HasForeignKey("CursoId");

                    b.HasOne("Tcc_Senai.Models.Funcionario", "Funcionario")
                        .WithMany("FuncionarioCursos")
                        .HasForeignKey("FuncionarioId");
                });

            modelBuilder.Entity("Tcc_Senai.Models.Turma", b =>
                {
                    b.HasOne("Tcc_Senai.Models.Curso", "Curso")
                        .WithMany("Turmas")
                        .HasForeignKey("IdCurso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}