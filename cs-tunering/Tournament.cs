namespace cs_tunering
{
    public class Tournament
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? PlayerCount { get; set; }
        public ICollection<ChildTournament>? ChildTournaments { get; set; } = new List<ChildTournament>();
        public bool IsParentTournament { get; set; } = true;
    }
}
