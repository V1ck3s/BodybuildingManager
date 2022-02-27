using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<ExerciceSeance> ExerciceSeances { get; set; }
        public DbSet<Exercice> Exercices { get; set; }
        public DbSet<Compte> Comptes { get; set; }

        public string DbPath { get; }

        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "database.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class Programme
    {
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }

        public List<Seance>? Seances { get; set; }
    }

    public class Seance
    {
        public string Nom { get; set; } = "";
        public List<ExerciceSeance>? Exercices { get; set; }
    }

    public class ExerciceSeance
    {
        public int Position { get; set; }
        public int NombreSerie { get; set; }
        public int NombreRepetition { get; set; }

    }

    public class Exercice
    {
        public String Nom { get; set; } = "";
    }

    public class Compte
    {
        public String Email { get; set; } = "";
        public String MotDePasse { get; set; } = "";
        public Programme? ProgrammeActuel { get; set; } = null;
        public List<Programme> ProgrammeAnciens { get; set; } = new List<Programme>();
    }
}