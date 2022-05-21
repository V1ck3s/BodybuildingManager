namespace BodybuildingManager.Lib.ObjetVue
{
    public class ObjetAjouterSeance
    {
        public BodybuildingManager.Models.Database.Programme Programme {get;set;} = new BodybuildingManager.Models.Database.Programme();
        public List<BodybuildingManager.Models.Database.Seance> ListeSeance {get;set;} = new List<BodybuildingManager.Models.Database.Seance>();
    }
}