using BodybuildingManager;

namespace BodybuildingManager.Lib.ObjetMetier
{
    public class Compte
    {
        public Guid Id { get; set; }
        public String Email { get; set; } = "";
        public String MotDePasse { get; set; } = "";
        public Programme? ProgrammeActuel { get; set; } = null;
        public List<Programme> ProgrammeAnciens { get; set; } = new List<Programme>();
    }
}
