namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionInfo
    {
        public int Id { get; set; }
        public string ChampionId { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int Difficulty { get; set; }

        public Champion Champion { get; set; }
    }
}
