
namespace BodybuildingManager.Lib.ObjetVue
{
    public class ObjetPoids
    {
        public BodybuildingManager.Lib.ObjetMetier.Poids PoidsMaintenant { get; set; } = new ObjetMetier.Poids();
        public DateTime DateMaintenant { get; set; } = DateTime.Now;
        public List<BodybuildingManager.Models.Database.Poids> ListePoids {get;set;} = new List<BodybuildingManager.Models.Database.Poids>();
    }
}
