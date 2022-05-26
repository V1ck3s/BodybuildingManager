using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BodybuildingManager.Models;
using BodybuildingManager.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BodybuildingManager.Controllers;

public class PoidsController : Controller
{
    private readonly ILogger<PoidsController> _logger;

    public PoidsController(ILogger<PoidsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Lib.ObjetVue.ObjetPoids PoidsUtilisateur = new Lib.ObjetVue.ObjetPoids();

        Compte? compte = Utils.CompteUtils.CompteParPseudo(Request.Cookies["PseudoConnecte"]);

        if (compte == null)
        { // Compte non trouvé
            TempData["message"] = "Erreur lors de l'affichage de la page des poids, le compte n'a pas pu être trouvé, veuillez vous reconnecter.";
            return RedirectToAction("Connexion", "Identification");
        }
        else
        { // Compte trouvé
            PoidsUtilisateur.ListePoids = compte.PoidsCompte;
            return View(PoidsUtilisateur);
        }
        
    }

    [HttpPost]
    public ActionResult PeseePoids(float? poidsKilo, DateTime? datePesee)
    {
        if (poidsKilo is null || datePesee is null)
        {
            return RedirectToAction("Index");
        }

        if (String.IsNullOrEmpty(Request.Cookies["PseudoConnecte"]))
        {
            return RedirectToAction("Connexion", "Identification");
        }

        Compte? compte = Utils.CompteUtils.AjoutePoids((float)poidsKilo,(DateTime)datePesee,Request.Cookies["PseudoConnecte"]);

        if (compte == null)
        { // Compte non trouvé
            TempData["message"] = "Erreur lors de l'ajout d'une pesée, le compte n'a pas été trouvé.";
            return RedirectToAction("Index");
        }
        else
        { // Compte trouvé
            return RedirectToAction("Index");
        }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult SupprimerPoids(Guid idPoids){
        using (var db = new DatabaseContext())
        {
            Poids poids = db.Poids.Find(idPoids);
            db.Poids.Remove(poids);
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
