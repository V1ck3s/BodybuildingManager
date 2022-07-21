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

                if(programmeActuel){
                    foreach(Programme p in compte.Programmes){
                        p.EstActif = false;
                    }
                }

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
                
                if(programmeActuel){
                    foreach(Programme p in db.Programmes){
                        p.EstActif = false;
                    }
                }

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

    public IActionResult AjouterSeance(Guid idProgramme)
    {
        using(var db = new DatabaseContext())
        {
            Programme programme = db.Programmes
            .Include(x=>x.Seances)
            .FirstOrDefault(c => c.Id == idProgramme);

            if (programme == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de la l'ajout d'une séance, le programme n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            { // Compte trouvé
                Lib.ObjetVue.ObjetAjouterSeance ObjetAjouterSeance = new Lib.ObjetVue.ObjetAjouterSeance();
                ObjetAjouterSeance.Programme = programme;
                ObjetAjouterSeance.ListeSeance = db.Seances.Where(x=>x.ProgrammeId == null).ToList();
                return View(ObjetAjouterSeance);
            }
        }
    }

    public IActionResult AjouterSeanceProgramme(Guid idSeance, Guid idProgramme)
    {
        using(var db = new DatabaseContext())
        {
            Programme programme = db.Programmes
            .Include(x=>x.Seances)
            .FirstOrDefault(c => c.Id == idProgramme);

            if (programme == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de la l'ajout d'une séance, le programme n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            { // Compte trouvé
                Seance seance = db.Seances
                .FirstOrDefault(c => c.Id == idSeance);

                if (seance == null)
                { // Compte non trouvé
                    TempData["message"] = "Erreur lors de la l'ajout d'une séance, la séance n'a pas pu être trouvée.";
                    return RedirectToAction("Index", "Programme");
                }
                else
                { // Compte trouvé

                    if(programme.Seances.Contains(seance))
                    {
                        TempData["message"] = "Erreur lors de la l'ajout d'une séance, la séance est déjà associée au programme.";
                        return RedirectToAction("Index", "Programme");
                    }
                    else
                    {
                        programme.Seances.Add(seance);
                        db.SaveChanges();

                        TempData["message"] = "La séance a bien été ajoutée au programme.";
                        return RedirectToAction("Index");
                    }
                }
            }
        }
    }

    public IActionResult RetirerSeance(Guid idProgramme, Guid idSeance)
    {
        using(var db = new DatabaseContext())
        {
            Programme programme = db.Programmes
            .Include(x=>x.Seances)
            .FirstOrDefault(c => c.Id == idProgramme);

            if (programme == null)
            { // Compte non trouvé
                TempData["message"] = "Erreur lors de la suppression de la séance du programme, le programme n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            { // Compte trouvé
                Seance seance = db.Seances
                .FirstOrDefault(c => c.Id == idSeance);

                if (seance == null)
                { // Compte non trouvé
                    TempData["message"] = "Erreur lors de la suppression de la séance du programme, la séance n'a pas pu être trouvée.";
                    return RedirectToAction("Index", "Programme");
                }
                else
                { // Compte trouvé

                    if(!programme.Seances.Contains(seance))
                    {
                        TempData["message"] = "Erreur lors de la suppression de la séance du programme, la séance n'est pas associée au programme.";
                        return RedirectToAction("Index", "Programme");
                    }
                    else
                    {
                        programme.Seances.Remove(seance);
                        db.SaveChanges();

                        TempData["message"] = "La séance a bien été retirée du programme.";
                        return RedirectToAction("Index");
                    }
                }
            }
        }
    }

    public IActionResult Dupliquer(Guid idProgramme)
    {
        using(var db = new DatabaseContext())
        {
            Programme programme = db.Programmes
            .Include(x=>x.Seances)
            .FirstOrDefault(c => c.Id == idProgramme);

            Compte? compte = db.Comptes
            .Include(x=>x.Programmes)
            .FirstOrDefault(c => c.Email == Request.Cookies["PseudoConnecte"]);

            if(compte == null)
            {
                TempData["message"] = "Erreur lors de la duplication du programme, le compte n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            {
                if (programme == null)
                { // Compte non trouvé
                    TempData["message"] = "Erreur lors de la duplication du programme, le programme n'a pas pu être trouvé.";
                    return RedirectToAction("Index", "Programme");
                }
                else
                { // Compte trouvé
                    Programme programmeDuplique = new Programme();
                    programmeDuplique.Nom = programme.Nom;
                    programmeDuplique.DateDebut = programme.DateDebut;
                    programmeDuplique.DateFin = programme.DateFin;
                    programmeDuplique.EstActif = false;
                    programmeDuplique.Seances = new List<Seance>();
                    programmeDuplique.Id = Guid.NewGuid();
                    
                    foreach(Seance s in programme.Seances){
                        Seance seance = new Seance();
                        seance.Nom = s.Nom;
                        seance.ProgrammeId = programmeDuplique.Id;
                        seance.Programme = programmeDuplique;
                        seance.Exercices = s.Exercices;
                        seance.Id = Guid.NewGuid();
                        programmeDuplique.Seances.Add(seance);
                    }
                    compte.Programmes.Add(programmeDuplique);
                    
                    db.SaveChanges();

                    TempData["message"] = "Le programme a bien été dupliqué.";
                    return RedirectToAction("Index");
                }
            }

            
        }
    }

    public IActionResult Supprimer(Guid idProgramme)
    {
        using(var db = new DatabaseContext())
        {
            Programme programme = db.Programmes
            .Include(x=>x.Seances)
            .FirstOrDefault(c => c.Id == idProgramme);

            Compte? compte = db.Comptes
            .Include(x=>x.Programmes)
            .FirstOrDefault(c => c.Email == Request.Cookies["PseudoConnecte"]);

            if(compte == null)
            {
                TempData["message"] = "Erreur lors de la suppression du programme, le compte n'a pas pu être trouvé.";
                return RedirectToAction("Index", "Programme");
            }
            else
            {
                if (programme == null)
                { // Compte non trouvé
                    TempData["message"] = "Erreur lors de la suppression du programme, le programme n'a pas pu être trouvé.";
                    return RedirectToAction("Index", "Programme");
                }
                else
                { // Compte trouvé
                    foreach(Seance s in programme.Seances){
                        db.Seances.Remove(s);
                    }
                    db.Programmes.Remove(programme);
                    db.SaveChanges();
                    TempData["message"] = "Le programme a bien été supprimé.";
                    return RedirectToAction("Index");
                }
            }
        }
    }

}
