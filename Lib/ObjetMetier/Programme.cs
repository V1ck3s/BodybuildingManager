namespace BodybuildingManager.Lib.ObjetMetier
{
    public class Programme
    {
        public Guid Id { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }

        public List<Seance>? Seances { get; set; }
    }
}
