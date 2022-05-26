using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BodybuildingManager.Models;
using BodybuildingManager.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BodybuildingManager.Controllers;

public class ParametreController : Controller
{
    private readonly ILogger<ParametreController> _logger;

    public ParametreController(ILogger<ParametreController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreerExercice(string nomExercice)
    {
        using (var db = new DatabaseContext())
        {
            if(db.Exercices.Any(x=>x.Nom == nomExercice))
            {
                return RedirectToAction("Index");
            }
            Exercice exercice = new Exercice();
            exercice.Nom = nomExercice;
            exercice.Id = Guid.NewGuid();
            db.Exercices.Add(exercice);
            db.SaveChanges();
        }
        return RedirectToAction("Index", "Parametre");
    }

    public IActionResult InjecterExercices(){

        using (var db = new DatabaseContext())
        {
            Models.ExerciceData.InsertExerciceData.InsertBaseExercice();
        }
        return RedirectToAction("Index", "Parametre");
    }
}