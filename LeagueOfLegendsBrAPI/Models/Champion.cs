using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public string Image_full { get; set; }
        public string Image_loading { get; set; }
        public string Image_square { get; set; }
        public ChampionInfo Info { get; set; }
        public ChampionStats Stats { get; set; } 
        public ChampionPassive Passive { get; set; }
        public virtual ICollection<ChampionSpell> Spells { get; set; }
        public virtual ICollection<ChampionSkin> Skins { get; set; }

    }
}
