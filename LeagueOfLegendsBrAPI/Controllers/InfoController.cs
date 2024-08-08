using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeagueOfLegendsBrAPI.Data;
using LeagueOfLegendsBrAPI.Models;
using LeagueOfLegendsBrAPI.Dtos;
using Newtonsoft.Json;

namespace LeagueOfLegendsBrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionInfoController : ControllerBase
    {
        private readonly ILogger<ChampionInfoController> _logger;
        private readonly LeagueOfLegendsContext _context;

        public ChampionInfoController(ILogger<ChampionInfoController> logger, LeagueOfLegendsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("test-connection")]
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
        public async Task<ActionResult<IEnumerable<InfoResDto>>> GetChampionsInfo()
        {
            var championsInfo = await _context.ChampionInfo
                .Include(ci => ci.Champion)
                .Select(c => new InfoResDto
                {
                    ChampionName = c.Champion.Name,
                    Attack = c.Attack,
                    Defense = c.Defense,
                    Magic = c.Magic,
                    Difficulty = c.Difficulty
                })
                .ToListAsync();

            return Ok(championsInfo);
        }

        [HttpGet("byChampionName/{championName}")]
        public async Task<ActionResult<IEnumerable<InfoResDto>>> GetChampionsInfoByName(string championName)
        {
            var championsInfo = await _context.ChampionInfo
                .Include(ci => ci.Champion)
                .Where(c => c.Champion.Name.Equals(championName, StringComparison.OrdinalIgnoreCase))
                .Select(c => new InfoResDto
                {
                    ChampionName = c.Champion.Name,
                    Attack = c.Attack,
                    Defense = c.Defense,
                    Magic = c.Magic,
                    Difficulty = c.Difficulty
                })
                .ToListAsync();

            if (!championsInfo.Any())
            {
                return NotFound(new { Message = "Champion not found." });
            }

            return Ok(championsInfo);
        }

    }
}