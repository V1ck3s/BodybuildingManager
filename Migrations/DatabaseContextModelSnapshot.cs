﻿// <auto-generated />
using System;
using BodybuildingManager.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BodybuildingManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("BodybuildingManager.Models.Database.Compte", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Exercice", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Exercices");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.ExerciceSeance", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ExerciceId")
                        .HasColumnType("TEXT");

                    b.Property<int>("NombreRepetition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NombreSerie")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("SeanceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExerciceId");

                    b.HasIndex("SeanceId");

                    b.ToTable("ExerciceSeances");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Poids", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DatePoids")
                        .HasColumnType("TEXT");

                    b.Property<float>("Kilogramme")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CompteId");

                    b.ToTable("Poids");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Programme", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateDebut")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EstActif")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompteId");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Seance", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProgrammeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("Seances");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.ExerciceSeance", b =>
                {
                    b.HasOne("BodybuildingManager.Models.Database.Exercice", "Exercice")
                        .WithMany()
                        .HasForeignKey("ExerciceId");

                    b.HasOne("BodybuildingManager.Models.Database.Seance", "Seance")
                        .WithMany("Exercices")
                        .HasForeignKey("SeanceId");

                    b.Navigation("Exercice");

                    b.Navigation("Seance");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Poids", b =>
                {
                    b.HasOne("BodybuildingManager.Models.Database.Compte", null)
                        .WithMany("PoidsCompte")
                        .HasForeignKey("CompteId");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Programme", b =>
                {
                    b.HasOne("BodybuildingManager.Models.Database.Compte", null)
                        .WithMany("Programmes")
                        .HasForeignKey("CompteId");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Seance", b =>
                {
                    b.HasOne("BodybuildingManager.Models.Database.Programme", "Programme")
                        .WithMany("Seances")
                        .HasForeignKey("ProgrammeId");

                    b.Navigation("Programme");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Compte", b =>
                {
                    b.Navigation("PoidsCompte");

                    b.Navigation("Programmes");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Programme", b =>
                {
                    b.Navigation("Seances");
                });

            modelBuilder.Entity("BodybuildingManager.Models.Database.Seance", b =>
                {
                    b.Navigation("Exercices");
                });
#pragma warning restore 612, 618
        }
    }
}
