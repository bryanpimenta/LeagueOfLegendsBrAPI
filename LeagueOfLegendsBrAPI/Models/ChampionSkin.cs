using System.ComponentModel.DataAnnotations;
namespace LeagueOfLegendsBrAPI.Models
{
    public class ChampionSkin
    {
        [Key]
        public int Id { get; set; }
        public string ChampionId { get; set; }
        public string Name { get; set; }
        public string Splash { get; set; }
        public string Centered { get; set; }
        public string Loading { get; set; }

        public Champion Champion { get; set; }
    }
}
