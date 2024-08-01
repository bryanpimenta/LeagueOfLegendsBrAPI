namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionPassive
    {
        public int Id { get; set; }
        public string ChampionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public Champion Champion { get; set; }
    }
}
