using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BodybuildingManager.Models;
using BodybuildingManager.Models.Database;
using Microsoft.EntityFrameworkCore;
using BodybuildingManager.Lib.ObjetVue;

namespace BodybuildingManager.Controllers;

public class SeanceController : Controller
{
    private readonly ILogger<ProgrammeController> _logger;

    public SeanceController(ILogger<ProgrammeController> logger)
    {
        _logger = logger;
    }

    public IActionResult CreerSeance()
    {
        Seance seance = new Seance();
        return View(seance);
    }


    
    public IActionResult Modifier(Guid idSeance)
    {
        using(var db = new DatabaseContext())
        {
            Seance seance = db.Seances
            .Include(x=>x.Exercices)
            .FirstOrDefault(s => s.Id == idSeance);

            if (seance == null)
            {
                TempData["message"] = "Erreur lors de la modification de la séance, la séance n'a pas pu être trouvée.";
                return RedirectToAction("Index", "Programme");
            }
            else
            {
                ObjetModifierSeance objetModifierSeance = new ObjetModifierSeance();
                objetModifierSeance.Seance = seance;
                objetModifierSeance.ListeExercice = db.Exercices.ToList();
                objetModifierSeance.ListeExerciceSeance = seance.Exercices.ToList();
                return View(objetModifierSeance);
            }
        }
    }

    [HttpPost]
    public IActionResult ModifierSeance(Guid idSeance, string seanceNom)
    {
        using(var db = new DatabaseContext())
        {
            Seance seance = db.Seances
            .Include(x=>x.Exercices)
            .FirstOrDefault(s => s.Id == idSeance);

            if (seance == null)
            {
                TempData["message"] = "Erreur lors de la modification de la séance, la séance n'a pas pu être trouvée.";
                return RedirectToAction("Index", "Programme");
            }
            else
            {
                seance.Nom = seanceNom;
                db.SaveChanges();
                TempData["message"] = "Séance modifiée avec succès.";
                return RedirectToAction("Index", "Programme");
            }
        }
    }

    [HttpPost]
    public IActionResult CreerSeance(string seanceNom)
    {
        using(var db = new DatabaseContext())
        {
            Compte? compte = db.Comptes
            .Include(x=>x.Programmes)
            .FirstOrDefault(c => c.Email == Request.Cookies["PseudoConnecte"]);

            if (compte == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de la création de la séance, le compte n'a pas pu être trouvé, veuillez vous reconnecter.";
                return RedirectToAction("Connexion", "Identification");
            }
            else
            { // Compte trouvé
                Seance seance = new Seance();
                seance.Nom = seanceNom;
                seance.Id = Guid.NewGuid();
                
                db.Seances.Add(seance);
                db.SaveChanges();
                return RedirectToAction("Index", "Programme");
            }
        }
    }

    [HttpPost]
    public IActionResult AjouterExercice(Guid idSeance, Guid idExercice, int nombreRepetition, int nombreSerie, int positionExercice)
    {
        using (var db = new DatabaseContext())
        {
            Seance seance = db.Seances.Include(x => x.Exercices).FirstOrDefault(x => x.Id == idSeance);
            Exercice exercice = db.Exercices.FirstOrDefault(x => x.Id == idExercice);

            if (seance == null)
            {
                TempData["message"] = "Erreur lors de l'ajout de l'exercice, la séance n'a pas pu être trouvée.";
                return RedirectToAction("Index", "Programme");
            }
            else if (exercice == null)
            {
                TempData["message"] = "Erreur lors de l'ajout de l'exercice, l'exercice n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            {
                ExerciceSeance exerciceSeance = new ExerciceSeance();
                exerciceSeance.Id = Guid.NewGuid();
                exerciceSeance.Exercice = exercice;
                exerciceSeance.Seance = seance;
                exerciceSeance.NombreRepetition = nombreRepetition;
                exerciceSeance.NombreSerie = nombreSerie;
                exerciceSeance.Position = positionExercice;

                db.Add(exerciceSeance);
                db.SaveChanges();
                return RedirectToAction("Index", "Programme");
            }
        }
    }

    [HttpPost]
    public IActionResult SupprimerExerciceSeance(Guid idExerciceSeance)
    {
        using (var db = new DatabaseContext())
        {
            ExerciceSeance? exerciceSeance = db.ExerciceSeances.FirstOrDefault(x => x.Id == idExerciceSeance);

            if (exerciceSeance == null)
            {
                TempData["message"] = "Erreur lors de la suppression de l'exercice, l'exercice n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            {
                db.Remove(exerciceSeance);
                db.SaveChanges();
                return RedirectToAction("Index", "Programme");
            }
        }
    }


    [HttpPost]
    public IActionResult ModifierExerciceSeance(Guid idExerciceSeance, int nombreRepetitionExercice, int nombreSerieExercice, int positionExercice){
        // A faire
        return View();
    }

}