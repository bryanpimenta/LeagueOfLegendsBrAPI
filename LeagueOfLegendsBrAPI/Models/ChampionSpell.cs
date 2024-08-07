using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionSpell
    {
        [Key]
        public int? Id { get; set; }
        public string? Key_board { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Champion_id { get; set; }

        [JsonIgnore]
        public Champion? Champion { get; set; }
    }
}
