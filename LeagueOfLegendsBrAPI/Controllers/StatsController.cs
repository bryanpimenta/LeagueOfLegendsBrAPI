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
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionStatsController : ControllerBase
    {
        private readonly ILogger<ChampionStatsController> _logger;
        private readonly LeagueOfLegendsContext _context;

        public ChampionStatsController(ILogger<ChampionStatsController> logger, LeagueOfLegendsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, List<StatsDto>>>> GetStatsGroupedByChampion()
        {
            var stats = await _context.ChampionStats
                .Include(s => s.Champion)
                .Select(s => new StatsResDto
                {
                    ChampionName = s.Champion.Name,
                    Hp = s.Hp,
                    HpPerLevel = s.HpperLevel,
                    Mp = s.Mp,
                    MpPerLevel = s.MpperLevel,
                    MoveSpeed = s.MoveSpeed,
                    Armor = s.Armor,
                    ArmorPerLevel = s.ArmorperLevel,
                    SpellBlock = s.SpellBlock,
                    SpellBlockPerLevel = s.SpellblockperLevel,
                    AttackRange = s.AttackRange,
                    HpRegen = s.HpRegen,
                    HpRegenPerLevel = s.HpregenperLevel,
                    MpRegen = s.MpRegen,
                    MpRegenPerLevel = s.MpregenperLevel,
                    Crit = s.Crit,
                    CritPerLevel = s.CritperLevel,
                    AttackDamage = s.AttackDamage,
                    AttackDamagePerLevel = s.AttackdamageperLevel,
                    AttackSpeed = s.AttackSpeed,
                    AttackSpeedPerLevel = s.AttackspeedperLevel
                })
                .ToListAsync();

            var groupedStats = stats
                .GroupBy(s => s.ChampionName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(s => new StatsDto
                    {
                        Hp = s.Hp,
                        HpPerLevel = s.HpPerLevel,
                        Mp = s.Mp,
                        MpPerLevel = s.MpPerLevel,
                        MoveSpeed = s.MoveSpeed,
                        Armor = s.Armor,
                        ArmorPerLevel = s.ArmorPerLevel,
                        SpellBlock = s.SpellBlock,
                        SpellBlockPerLevel = s.SpellBlockPerLevel,
                        AttackRange = s.AttackRange,
                        HpRegen = s.HpRegen,
                        HpRegenPerLevel = s.HpRegenPerLevel,
                        MpRegen = s.MpRegen,
                        MpRegenPerLevel = s.MpRegenPerLevel,
                        Crit = s.Crit,
                        CritPerLevel = s.CritPerLevel,
                        AttackDamage = s.AttackDamage,
                        AttackDamagePerLevel = s.AttackDamagePerLevel,
                        AttackSpeed = s.AttackSpeed,
                        AttackSpeedPerLevel = s.AttackSpeedPerLevel
                    }).ToList());

            if (!stats.Any())
            {
                return NotFound();
            }

            return Ok(groupedStats);
        }

        [HttpGet("byChampion/{championName}")]
        public async Task<ActionResult<Dictionary<string, List<StatsDto>>>> GetStatsByChampion(string championName)
        {
            var stats = await _context.ChampionStats
                .Include(s => s.Champion)
                .Where(s => s.Champion.Name == championName)
                .Select(s => new StatsResDto
                {
                    ChampionName = s.Champion.Name,
                    Hp = s.Hp,
                    HpPerLevel = s.HpperLevel,
                    Mp = s.Mp,
                    MpPerLevel = s.MpperLevel,
                    MoveSpeed = s.MoveSpeed,
                    Armor = s.Armor,
                    ArmorPerLevel = s.ArmorperLevel,
                    SpellBlock = s.SpellBlock,
                    SpellBlockPerLevel = s.SpellblockperLevel,
                    AttackRange = s.AttackRange,
                    HpRegen = s.HpRegen,
                    HpRegenPerLevel = s.HpregenperLevel,
                    MpRegen = s.MpRegen,
                    MpRegenPerLevel = s.MpregenperLevel,
                    Crit = s.Crit,
                    CritPerLevel = s.CritperLevel,
                    AttackDamage = s.AttackDamage,
                    AttackDamagePerLevel = s.AttackdamageperLevel,
                    AttackSpeed = s.AttackSpeed,
                    AttackSpeedPerLevel = s.AttackspeedperLevel
                })
                .ToListAsync();

            if (!stats.Any())
            {
                return NotFound();
            }

            var result = new Dictionary<string, List<StatsDto>>
            {
                {
                    championName, stats.Select(s => new StatsDto
                    {
                        Hp = s.Hp,
                        HpPerLevel = s.HpPerLevel,
                        Mp = s.Mp,
                        MpPerLevel = s.MpPerLevel,
                        MoveSpeed = s.MoveSpeed,
                        Armor = s.Armor,
                        ArmorPerLevel = s.ArmorPerLevel,
                        SpellBlock = s.SpellBlock,
                        SpellBlockPerLevel = s.SpellBlockPerLevel,
                        AttackRange = s.AttackRange,
                        HpRegen = s.HpRegen,
                        HpRegenPerLevel = s.HpRegenPerLevel,
                        MpRegen = s.MpRegen,
                        MpRegenPerLevel = s.MpRegenPerLevel,
                        Crit = s.Crit,
                        CritPerLevel = s.CritPerLevel,
                        AttackDamage = s.AttackDamage,
                        AttackDamagePerLevel = s.AttackDamagePerLevel,
                        AttackSpeed = s.AttackSpeed,
                        AttackSpeedPerLevel = s.AttackSpeedPerLevel
                    }).ToList()
                }
            };

            if (!result.Any())
            {
                return NotFound($"Nenhum campeão encontrado com o nome '{championName}'.");
            }
            
            return Ok(result);
        }
    }
}
