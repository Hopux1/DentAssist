﻿// <auto-generated />
using System;
using DentAssist.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DentAssist.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DentAssist.Models.Odontologo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Odontologos");
                });

            modelBuilder.Entity("DentAssist.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prevision")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("DentAssist.Models.PasoTratamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("FechaEstimada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PlanTratamientoId")
                        .HasColumnType("int");

                    b.Property<int>("TratamientoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanTratamientoId");

                    b.HasIndex("TratamientoId");

                    b.ToTable("PasosTratamiento");
                });

            modelBuilder.Entity("DentAssist.Models.PlanTratamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("OdontologoId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OdontologoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("PlanesTratamiento");
                });

            modelBuilder.Entity("DentAssist.Models.Tratamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Tratamientos");
                });

            modelBuilder.Entity("DentAssist.Models.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DuracionMinutos")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("OdontologoId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OdontologoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("DentAssist.Models.PasoTratamiento", b =>
                {
                    b.HasOne("DentAssist.Models.PlanTratamiento", "PlanTratamiento")
                        .WithMany("Pasos")
                        .HasForeignKey("PlanTratamientoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentAssist.Models.Tratamiento", "Tratamiento")
                        .WithMany("Pasos")
                        .HasForeignKey("TratamientoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlanTratamiento");

                    b.Navigation("Tratamiento");
                });

            modelBuilder.Entity("DentAssist.Models.PlanTratamiento", b =>
                {
                    b.HasOne("DentAssist.Models.Odontologo", "Odontologo")
                        .WithMany("PlanesTratamiento")
                        .HasForeignKey("OdontologoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentAssist.Models.Paciente", "Paciente")
                        .WithMany("PlanesTratamiento")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Odontologo");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("DentAssist.Models.Turno", b =>
                {
                    b.HasOne("DentAssist.Models.Odontologo", "Odontologo")
                        .WithMany("Turnos")
                        .HasForeignKey("OdontologoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentAssist.Models.Paciente", "Paciente")
                        .WithMany("Turnos")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Odontologo");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("DentAssist.Models.Odontologo", b =>
                {
                    b.Navigation("PlanesTratamiento");

                    b.Navigation("Turnos");
                });

            modelBuilder.Entity("DentAssist.Models.Paciente", b =>
                {
                    b.Navigation("PlanesTratamiento");

                    b.Navigation("Turnos");
                });

            modelBuilder.Entity("DentAssist.Models.PlanTratamiento", b =>
                {
                    b.Navigation("Pasos");
                });

            modelBuilder.Entity("DentAssist.Models.Tratamiento", b =>
                {
                    b.Navigation("Pasos");
                });
#pragma warning restore 612, 618
        }
    }
}
