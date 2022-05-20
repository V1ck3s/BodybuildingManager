using BodybuildingManager.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BodybuildingManager.Utils;

public class CompteUtils{
    public static Compte? CompteParPseudo(string pseudo){
        using (var db = new DatabaseContext()){
            Compte? compte = db.Comptes
            .Include(x=>x.PoidsCompte)
            .Include(x=>x.Programmes)
            .FirstOrDefault(c => c.Email == pseudo);

            if (compte == null) // Compte non trouvé
            {
                return null;
            }
            else // Compte trouvé
            {
                return compte;
            }
        }
    }
    public static Compte? AjoutePoids(float poidsKilo, DateTime datePesee, string pseudo){
        using (var db = new DatabaseContext()){
            Compte? compte = db.Comptes
            .Include(x=>x.PoidsCompte)
            .FirstOrDefault(c => c.Email == pseudo);

            if (compte == null) // Compte non trouvé
            {
                return null;
            }
            else // Compte trouvé
            {
                Poids poids = new Poids();
                poids.Kilogramme = poidsKilo;
                poids.DatePoids = datePesee;
                compte.PoidsCompte.Add(poids);
                db.SaveChanges();
                return compte;
            }
        }
    }
}