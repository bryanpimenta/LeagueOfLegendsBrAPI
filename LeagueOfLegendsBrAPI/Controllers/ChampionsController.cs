using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeagueOfLegendsBrAPI.Data;
using LeagueOfLegendsBrAPI.Models;
using LeagueOfLegendsBrAPI.Dtos;
using Microsoft.Extensions.Logging;

namespace LeagueOfLegendsBrAPI.Controllers
{
    [Route("api/v1/champions")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        private readonly ILogger<ChampionsController> _logger;
        private readonly LeagueOfLegendsContext _context;

        public ChampionsController(ILogger<ChampionsController> logger, LeagueOfLegendsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var champions = await _context.Champion.ToListAsync();
                return Ok(new { Message = "Database connection successful", ChampionsCount = champions.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Database connection failed", Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, ChampionDto>>> GetChampions()
        {
            var champions = await _context.Champion
                .Select(c => new ChampionDto
                {
                    Key = c.Key,
                    Name = c.Name,
                    Title = c.Title,
                    Lore = c.Lore,
                    Tags = c.Tags,
                    Partype = c.Partype,
                    Image_full = c.Image_full,
                    Image_loading = c.Image_loading,
                    Image_square = c.Image_square,
                }).ToListAsync();

            
            var result = champions.ToDictionary(c => c.Name, c => c);

            return Ok(result);
        }

        [HttpGet("{championName}")]
        public async Task<ActionResult<Dictionary<string, ChampionDto>>> GetChampionByName(string championName)
        {
            championName = char.ToUpper(championName.ToLower()[0]) + championName.Substring(1).ToLower();

            var champions = await _context.Champion
                .Where(c => c.Name.Contains(championName, StringComparison.OrdinalIgnoreCase))
                .Select(c => new ChampionDto
                {
                    Key = c.Key,
                    Name = c.Name,
                    Title = c.Title,
                    Lore = c.Lore,
                    Tags = c.Tags,
                    Partype = c.Partype,
                    Image_full = c.Image_full,
                    Image_loading = c.Image_loading,
                    Image_square = c.Image_square,
                }).ToListAsync();

            
            var result = champions.ToDictionary(c => c.Name, c => c);

            if (!result.Any())
            {
                return NotFound($"Nenhum campeão encontrado com o nome '{championName}'.");
            }

            return Ok(result);
        }
    }
}
