namespace BodybuildingManager.Lib.ObjetVue
{
    public class ObjetModifierSeance
    {
        public List<BodybuildingManager.Models.Database.Exercice> ListeExercice { get; set; } = new List<BodybuildingManager.Models.Database.Exercice>();
        public BodybuildingManager.Models.Database.Seance Seance {get;set;} = new Models.Database.Seance();
        public List<BodybuildingManager.Models.Database.ExerciceSeance> ListeExerciceSeance { get; set; } = new List<BodybuildingManager.Models.Database.ExerciceSeance>();
    }
}