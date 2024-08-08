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
    public class ChampionSkinController : ControllerBase
    {
        private readonly ILogger<ChampionSkinController> _logger;
        private readonly LeagueOfLegendsContext _context;

        public ChampionSkinController(ILogger<ChampionSkinController> logger, LeagueOfLegendsContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<Dictionary<string, List<SkinDto>>>> GetSkinsGroupedByChampion()
        {
            var skins = await _context.ChampionSkin
                .Include(s => s.Champion)
                .Select(s => new SkinResDto
                {
                    ChampionName = s.Champion.Name,
                    Name = s.Name,
                    Splash = s.Splash,
                    Loading = s.Loading,
                    Model_view = s.Model_view
                })
                .ToListAsync();

            var groupedSkins = skins
                .GroupBy(s => s.ChampionName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(s => new SkinDto
                    {
                        Name = s.Name,
                        Splash = s.Splash,
                        Loading = s.Loading,
                        Model_view = s.Model_view
                    }).ToList());

            return Ok(groupedSkins);
        }


        [HttpGet("byChampion/{championName}")]
        public async Task<ActionResult<Dictionary<string, List<SkinDto>>>> GetSkinsByChampion(string championName)
        {
            var skins = await _context.ChampionSkin
                .Where(s => s.Champion.Name == championName)
                .Select(s => new SkinResDto
                {
                    Name = s.Name,
                    ChampionName = s.Champion.Name,
                    Splash = s.Splash,
                    Loading = s.Loading,
                    Model_view = s.Model_view
                })
                .ToListAsync();

            if (!skins.Any())
            {
                return NotFound();
            }

            var result = new Dictionary<string, List<SkinDto>>
            {
                { 
                    championName, skins.Select(s => new SkinDto
                    {
                        Name = s.Name,
                        Splash = s.Splash,
                        Loading = s.Loading,
                        Model_view = s.Model_view
                    }).ToList() 
                }
            };

            return Ok(result);
        }
    }
}
