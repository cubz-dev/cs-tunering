using System.Text.Json.Serialization;

namespace cs_tunering
{
    public class ChildTournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PlayerCount { get; set; }
        public int ParentTournamentId { get; set; }

        [JsonIgnore]
        public Tournament ParentTournament { get; set; } 
    }
}
