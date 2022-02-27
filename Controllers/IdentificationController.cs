using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BodybuildingManager.Models;
using BodybuildingManager.Models.Database;

namespace BodybuildingManager.Controllers;

public class IdentificationController : Controller
{
    public IActionResult Connexion()
    {
        return View();
    }

    public IActionResult Inscription()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Connexion(string email = "", string motdepasse = "")
    {
        using (var db = new DatabaseContext())
        {
            Compte? compte = db.Comptes
                .Where(b => b.Email == email && b.MotDePasse == motdepasse)
                .ToList().FirstOrDefault();
                
            if (compte == null)
            { // Echec de connexion
                ViewBag.Message = "Erreur lors de la connexion, l'e-mail ou le mot de passe est incorrecte.";
                return View();
            }
            else
            { // Connexion réussie
                return RedirectToAction("Index","Home");
            }
        }
    }
    [HttpPost]
    public ActionResult Inscription(string email = "", string motdepasse = "")
    {
        using (var db = new DatabaseContext())
        {
            Compte compte = new Compte { Id = Guid.NewGuid(), Email = email, MotDePasse = motdepasse, ProgrammeActuel = null, ProgrammeAnciens = new List<Programme>() };
            db.Comptes.Add(compte);
            db.SaveChanges();
        }
        ViewBag.Message = "Inscription réussie, veuillez vous connecter.";
        return RedirectToAction("Connexion","Identification");
    }
}