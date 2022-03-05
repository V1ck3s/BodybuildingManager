using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BodybuildingManager.Models;
using BodybuildingManager.Models.Database;

namespace BodybuildingManager.Controllers;

public class IdentificationController : Controller
{
    public IActionResult Connexion()
    {
        ViewBag.Message = TempData["message"];
        return View();
    }

    public IActionResult Deconnexion()
    {
        Response.Cookies.Delete("PseudoConnecte");
        return RedirectToAction("Connexion", "Identification");
    }

    public IActionResult Inscription()
    {
        return View();
    }
    public IActionResult Compte()
    {
        if (String.IsNullOrEmpty(Request.Cookies["PseudoConnecte"]))
        {
            return RedirectToAction("Connexion", "Identification");
        }
        Compte? compte = null;
        using (var db = new DatabaseContext())
        {
            compte = db.Comptes
                .Where(b => b.Email == Request.Cookies["PseudoConnecte"])
                .ToList().FirstOrDefault();
        }
        return View(compte);
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
                TempData["message"] = "Erreur lors de la connexion, l'e-mail ou le mot de passe est incorrecte.";
                return View();
            }
            else
            { // Connexion réussie
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Append("PseudoConnecte", email, option);
                return RedirectToAction("Compte", "Identification");
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
        TempData["message"] = "Inscription réussie, veuillez vous connecter.";
        return RedirectToAction("Connexion", "Identification");
    }
}