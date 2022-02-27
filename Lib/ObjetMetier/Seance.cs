
namespace BodybuildingManager.Lib.ObjetMetier
{
    public class Seance
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = "";
        public List<ExerciceSeance>? Exercices { get; set; }
    }
}
