using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BodybuildingManager.Models;
using BodybuildingManager.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BodybuildingManager.Controllers;

public class ProgrammeController : Controller
{
    private readonly ILogger<ProgrammeController> _logger;

    public ProgrammeController(ILogger<ProgrammeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Lib.ObjetVue.ObjetProgramme ProgrammesUtilisateur = new Lib.ObjetVue.ObjetProgramme();
        ProgrammesUtilisateur.ListeProgramme = new List<Programme>();

        Compte? compte = Utils.CompteUtils.CompteParPseudo(Request.Cookies["PseudoConnecte"]);

        if (compte == null)
        { // Compte non trouvé
            TempData["message"] = "Erreur lors de l'affichage de la page des programmes, le compte n'a pas pu être trouvé, veuillez vous reconnecter.";
            return RedirectToAction("Connexion", "Identification");
        }
        else
        { // Compte trouvé
            ProgrammesUtilisateur.ListeProgramme = compte.Programmes;
            return View(ProgrammesUtilisateur);
        }
    }

    public IActionResult CreerProgramme()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreerProgramme(string programmeNom, DateTime programmeDateDebut, DateTime? programmeDateFin, bool programmeActuel)
    {
        using(var db = new DatabaseContext())
        {

            Compte? compte = db.Comptes
            .Include(x=>x.Programmes)
            .FirstOrDefault(c => c.Email == Request.Cookies["PseudoConnecte"]);


            if (compte == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de la création du programme, le compte n'a pas pu être trouvé, veuillez vous reconnecter.";
                return RedirectToAction("Connexion", "Identification");
            }
            else
            { // Compte trouvé
                Programme programme = new Programme();
                programme.Nom = programmeNom;
                programme.DateDebut = programmeDateDebut;
                programme.DateFin = programmeDateFin;
                programme.EstActif = programmeActuel;
                programme.Id = Guid.NewGuid();

                
                compte.Programmes.Add(programme);
                db.SaveChanges();
                

                TempData["message"] = "Le programme a bien été créé.";
                return RedirectToAction("Index");
            }
        }
    }

    public IActionResult Modifier(Guid id){
        using(var db = new DatabaseContext())
        {
            Programme programme = db.Programmes
            .Include(x=>x.Seances)
            .FirstOrDefault(c => c.Id == id);

            if (programme == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de la modification du programme, le programme n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            { // Compte trouvé
                return View(programme);
            }
        }
    }

    [HttpPost]
    public IActionResult ModifierProgramme(Guid idProgramme, string programmeNom, DateTime programmeDateDebut, DateTime? programmeDateFin, bool programmeActuel)
    {
        using(var db = new DatabaseContext())
        {
            Programme programme = db.Programmes
            .Include(x=>x.Seances)
            .FirstOrDefault(c => c.Id == idProgramme);

            if (programme == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de la modification du programme, le programme n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            { // Compte trouvé
                programme.Nom = programmeNom;
                programme.DateDebut = programmeDateDebut;
                programme.DateFin = programmeDateFin;
                programme.EstActif = programmeActuel;

                db.SaveChanges();

                TempData["message"] = "Le programme a bien été modifié.";
                return RedirectToAction("Index");
            }
        }
    }

}
