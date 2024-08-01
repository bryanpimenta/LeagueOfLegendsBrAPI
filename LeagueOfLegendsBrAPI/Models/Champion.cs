using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeagueOfLegendsBrAPI.Models
{
    public class Champion
    {
        [Key]
        public string Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Lore { get; set; }
        public string Tags { get; set; }
        public string Partype { get; set; }
        public string image_full { get; set; }
        public string image_loading { get; set; }
        public string image_square { get; set; }
        public ICollection<ChampionSkin> Skins { get; set; }
        public ChampionInfo Info { get; set; }
        public ChampionStats Stats { get; set; }
        public ICollection<ChampionSpell> Spells { get; set; }
        public ChampionPassive Passive { get; set; }
    }
}
