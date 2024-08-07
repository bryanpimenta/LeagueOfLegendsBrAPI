using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionStats
    {
        [Key]
        public int Id { get; set; }
        public string? Champion_id { get; set; }
        public int Hp { get; set; }
        public int HpPerLevel { get; set; }
        public int Mp { get; set; }
        public int MpPerLevel { get; set; }
        public int MoveSpeed { get; set; }
        public int Armor { get; set; }
        public int ArmorPerLevel { get; set; }
        public int SpellBlock { get; set; }
        public int SpellBlockPerLevel { get; set; }
        public int AttackRange { get; set; }
        public int HpRegen { get; set; }
        public int HpRegenPerLevel { get; set; }
        public int MpRegen { get; set; }
        public int MpRegenPerLevel { get; set; }
        public int Crit { get; set; }
        public int CritPerLevel { get; set; }
        public int AttackDamage { get; set; }
        public int AttackDamagePerLevel { get; set; }
        public int AttackSpeedPerLevel { get; set; }
        public int AttackSpeed { get; set; }

        [JsonIgnore]
        public Champion? Champion { get; set; }
    }
}
