namespace LeagueOfLegendsBrAPI.Dtos {
    public class InfoDto
    {
        public int? Attack { get; set; }
        public int? Defense { get; set; }
        public int? Magic { get; set; }
        public int? Difficulty { get; set; }
    }

        public class InfoResDto : InfoDto
    {
        public string? ChampionName { get; set; }
    }
}