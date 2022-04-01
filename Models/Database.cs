using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BodybuildingManager.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<ExerciceSeance> ExerciceSeances { get; set; }
        public DbSet<Exercice> Exercices { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Poids> Poids { get; set; }

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
        public Guid Id { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }

        public List<Seance>? Seances { get; set; }
    }

    public class Seance
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = "";
        public List<ExerciceSeance>? Exercices { get; set; }
    }

    public class ExerciceSeance
    {
        public Guid Id {get;set;}
        public Exercice? Exercice { get; set; }
        public Seance? Seance { get; set; }
        public int Position { get; set; }
        public int NombreSerie { get; set; }
        public int NombreRepetition { get; set; }

    }

    public class Exercice
    {
        public Guid Id { get; set; }
        public String Nom { get; set; } = "";
    }

    public class Compte
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public String Email { get; set; } = "";
        public String MotDePasse { get; set; } = "";
        public Programme? ProgrammeActuel { get; set; } = null;
        public List<Programme> ProgrammeAnciens { get; set; } = new List<Programme>();
        public List<Poids> PoidsCompte {get;set;} = new List<Poids>();
    }
    public class Poids{
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id {get;set;} = Guid.NewGuid();
        public float Kilogramme {get;set;}
        public DateTime DatePoids {get;set;}
    }
}