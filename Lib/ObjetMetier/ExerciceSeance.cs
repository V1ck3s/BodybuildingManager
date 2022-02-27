namespace BodybuildingManager.Lib.ObjetMetier
{
    public class ExerciceSeance
    {
        public Guid Id { get; set; }
        public Exercice? Exercice { get; set; }
        public Seance? Seance { get; set; }
        public int Position { get; set; }
        public int NombreSerie { get; set; }
        public int NombreRepetition { get; set; }

    }
}
