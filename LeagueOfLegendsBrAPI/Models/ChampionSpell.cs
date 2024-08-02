using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionSpell
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string champion_id { get; set; }

        [JsonIgnore]
        public Champion Champion { get; set; }
    }
}
