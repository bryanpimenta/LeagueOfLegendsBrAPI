using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionSkin
    {
        [Key]
        public int? Id { get; set; }
        public string? Champion_id { get; set; } 
        public string? Name { get; set; }
        public string? Splash { get; set; }
        public string? Centered { get; set; }
        public string? Loading { get; set; }
        public string? Model_view { get; set; }

        [JsonIgnore]
        public Champion? Champion { get; set; }
    }
}
