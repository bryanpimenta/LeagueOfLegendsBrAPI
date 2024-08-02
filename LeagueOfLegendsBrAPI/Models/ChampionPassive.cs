using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionPassive
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey("champion_id")]     
        public string Champion_id { get; set; }

        [JsonIgnore]
        public Champion Champion { get; set; }
    }
}
