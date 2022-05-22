using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BodybuildingManager.Models.Database;

namespace BodybuildingManager.Models.ExerciceData
{

    public class InsertExerciceData
    {
        public void InsertExercice(Exercice exercice)
        {
            using(var db = new DatabaseContext())
            {
                db.Exercices.Add(exercice);
                db.SaveChanges();
            }
            
        }

        public void InsertExercice(List<Exercice> exercices){
            using(var db = new DatabaseContext())
            {
                db.Exercices.AddRange(exercices);
                db.SaveChanges();
            }
        }

        public void InsertBaseExercice(){
            List<Exercice> exercices = new List<Exercice>{
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Développé couché haltère"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Développé couché barre"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Écarté couché haltère"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Crunch abdominal"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Tirage sol poulie basse"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Tirage poitrine"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Shrug barre"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Traction à la barre fixe"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Développé haltère"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Développé Arnold"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Élevation latérales"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Leg extensioon"
                },

                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Leg curl couché"
                },

                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Mollets debout"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Curl haltère"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Curl haltère pupitre"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Extensions à la poulie haute"
                },
                new Exercice{
                    Id = Guid.NewGuid(),
                    Nom = "Dips banc"
                },

            };
        }
        
    }
}