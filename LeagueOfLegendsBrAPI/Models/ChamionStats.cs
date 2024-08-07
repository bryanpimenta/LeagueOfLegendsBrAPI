using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionStats
    {
        [Key]
        public int? Id { get; set; }
        public string? Champion_id { get; set; }
        public int? Hp { get; set; }
        public int? HpperLevel { get; set; }
        public int? Mp { get; set; }
        public int? MpperLevel { get; set; }
        public int? MoveSpeed { get; set; }
        public int? Armor { get; set; }
        public int? ArmorperLevel { get; set; }
        public int? SpellBlock { get; set; }
        public int? SpellblockperLevel { get; set; }
        public int? AttackRange { get; set; }
        public int? HpRegen { get; set; }
        public int? HpregenperLevel { get; set; }
        public int? MpRegen { get; set; }
        public int? MpregenperLevel { get; set; }
        public int? Crit { get; set; }
        public int? CritperLevel { get; set; }
        public int? AttackDamage { get; set; }
        public int? AttackdamageperLevel { get; set; }
        public int? AttackspeedperLevel { get; set; }
        public int? AttackSpeed { get; set; }

        [JsonIgnore]
        public Champion? Champion { get; set; }
    }
}
