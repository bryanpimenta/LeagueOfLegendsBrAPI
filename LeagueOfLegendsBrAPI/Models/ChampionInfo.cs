using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace LeagueOfLegendsBrAPI.Models
{

    public class ChampionInfo
    {
        public int Id { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int Difficulty { get; set; }

        [ForeignKey("champion_id")] 
        public string Champion_id { get; set; }

        [JsonIgnore]
        public Champion Champion { get; set; }
    }
}
