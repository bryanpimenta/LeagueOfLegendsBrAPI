namespace LeagueOfLegendsBrAPI.Dtos
{
    public class ChampionDto
    {
        public string? Key { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Lore { get; set; }
        public string? Tags { get; set; }
        public string? Partype { get; set; }
        public string? Image_full { get; set; }
        public string? Image_loading { get; set; }
        public string? Image_square { get; set; }
    }

    public class ChampionFullDataDto : ChampionDto
    {
        public InfoDto? Info { get; set; }
        public StatsDto? Stats { get; set; }
        public PassiveDto? Passive { get; set; }
        public List<SpellDto>? Spells { get; set; }
        public List<SkinDto>? Skins { get; set; }
    }

    public class ChampionFullDataResDto
    {
        public ChampionDto? Details { get; set; }
        public InfoDto? Info { get; set; }
        public StatsDto? Stats { get; set; }
        public PassiveDto? Passive { get; set; }
        public List<SpellDto>? Spells { get; set; }
        public List<SkinDto>? Skins { get; set; }
    }

}