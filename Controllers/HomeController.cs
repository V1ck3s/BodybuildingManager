using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BodybuildingManager.Models;
using BodybuildingManager.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BodybuildingManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Poids()
    {
        Lib.ObjetVue.ObjetPoids PoidsUtilisateur = new Lib.ObjetVue.ObjetPoids();
        
        string pseudo = Request.Cookies["PseudoConnecte"].ToString();

        using (var db = new DatabaseContext())
        {
            Compte? compte = db.Comptes.Include(x=>x.PoidsCompte).FirstOrDefault(c => c.Email == pseudo);
            //db.Entry(compte).Reference(c => c.PoidsCompte).Load();
            //db.Entry(compte).Collection(c => c.PoidsCompte).Load();
            if (compte == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de l'affichage de la page des poids, le compte n'a pas pu être trouvé.";
                return View();
            }
            else
            { // Compte trouvé
                
                PoidsUtilisateur.ListePoids = compte.PoidsCompte;

                
            }
        }

        return View(PoidsUtilisateur);
    }

    [HttpPost]
    public ActionResult PeseePoids(float? poidsKilo, DateTime? datePesee)
    {
        if (poidsKilo is null || datePesee is null)
        {
            return RedirectToAction("Poids");
        }

        if (String.IsNullOrEmpty(Request.Cookies["PseudoConnecte"]))
        {
            return RedirectToAction("Connexion", "Identification");
        }

        string pseudo = Request.Cookies["PseudoConnecte"].ToString();

        using (var db = new DatabaseContext())
        {
            Compte? compte = db.Comptes
                .Where(b => b.Email == pseudo)
                .ToList().FirstOrDefault();

            if (compte == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de l'ajout d'une pesée, le compte n'a pas été trouvé.";
                return RedirectToAction("Poids");
            }
            else
            { // Compte trouvé
                Poids poids = new Poids();
                poids.DatePoids = (DateTime)datePesee;
                poids.Kilogramme = (float)poidsKilo;
                compte.PoidsCompte.Add(poids);
                //db.Comptes.Find(compte.Id).PoidsCompte.Add(poids);
                db.SaveChanges();
                return RedirectToAction("Poids");
            }
        }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
