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
    [Route("api/v1/champions/full")]
    [ApiController]
    public class ChampionsFullDataController : ControllerBase
    {
        private readonly ILogger<ChampionsFullDataController> _logger;
        private readonly LeagueOfLegendsContext _context;

        public ChampionsFullDataController(ILogger<ChampionsFullDataController> logger, LeagueOfLegendsContext context)
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
        public async Task<ActionResult<Dictionary<string, ChampionFullDataResDto>>> GetChampionsFullData()
        {
            var champions = await _context.Champion
                .Select(c => new ChampionFullDataResDto
                {
                    Details = new ChampionDto
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
                    },
                    Info = new InfoDto
                    {
                        Attack = c.Info.Attack,
                        Defense = c.Info.Defense,
                        Magic = c.Info.Magic,
                        Difficulty = c.Info.Difficulty
                    },
                    Stats = new StatsDto
                    {
                        Hp = c.Stats.Hp,
                        HpPerLevel = c.Stats.HpperLevel,
                        Mp = c.Stats.Mp,
                        MpPerLevel = c.Stats.MpperLevel,
                        MoveSpeed = c.Stats.MoveSpeed,
                        AttackRange = c.Stats.AttackRange,
                        Armor = c.Stats.Armor,
                        ArmorPerLevel = c.Stats.ArmorperLevel,
                        SpellBlock = c.Stats.SpellBlock,
                        SpellBlockPerLevel = c.Stats.SpellblockperLevel,
                        HpRegen = c.Stats.HpRegen,
                        HpRegenPerLevel = c.Stats.HpregenperLevel,
                        MpRegen = c.Stats.MpRegen,
                        MpRegenPerLevel = c.Stats.MpregenperLevel,
                        Crit = c.Stats.Crit,
                        CritPerLevel = c.Stats.CritperLevel,
                        AttackDamage = c.Stats.AttackDamage,
                        AttackDamagePerLevel = c.Stats.AttackdamageperLevel,
                        AttackSpeed = c.Stats.AttackSpeed,
                        AttackSpeedPerLevel = c.Stats.AttackspeedperLevel
                    },
                    Passive = new PassiveDto
                    {
                        Name = c.Passive.Name,
                        Description = c.Passive.Description,
                        Image = c.Passive.Image
                    },
                    Spells = c.Spells.Select(s => new SpellDto
                    {
                        Key_board = s.Key_board,
                        Name = s.Name,
                        Description = s.Description,
                        Image = s.Image
                    }).ToList(),
                    Skins = c.Skins.Select(s => new SkinDto
                    {
                        Name = s.Name,
                        Splash = s.Splash,
                        Loading = s.Loading,
                        Model_view = s.Model_view
                    }).ToList()
                })
                .ToListAsync();

            var result = champions.ToDictionary(c => c.Details!.Name, c => c);

            return Ok(result);
        }



        [HttpGet("{championName}")]
        public async Task<ActionResult<Dictionary<string, ChampionFullDataResDto>>> GetChampionFullDataByName(string championName)
        {
            championName = char.ToUpper(championName.ToLower()[0]) + championName.Substring(1).ToLower();

            var champions = await _context.Champion
                .Where(c => c.Name.Contains(championName, StringComparison.OrdinalIgnoreCase))
                .Select(c => new ChampionFullDataResDto
                {
                    Details = new ChampionDto
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
                    },
                    Info = new InfoDto
                    {
                        Attack = c.Info.Attack,
                        Defense = c.Info.Defense,
                        Magic = c.Info.Magic,
                        Difficulty = c.Info.Difficulty
                    },
                    Stats = new StatsDto
                    {
                        Hp = c.Stats.Hp,
                        HpPerLevel = c.Stats.HpperLevel,
                        Mp = c.Stats.Mp,
                        MpPerLevel = c.Stats.MpperLevel,
                        MoveSpeed = c.Stats.MoveSpeed,
                        AttackRange = c.Stats.AttackRange,
                        Armor = c.Stats.Armor,
                        ArmorPerLevel = c.Stats.ArmorperLevel,
                        SpellBlock = c.Stats.SpellBlock,
                        SpellBlockPerLevel = c.Stats.SpellblockperLevel,
                        HpRegen = c.Stats.HpRegen,
                        HpRegenPerLevel = c.Stats.HpregenperLevel,
                        MpRegen = c.Stats.MpRegen,
                        MpRegenPerLevel = c.Stats.MpregenperLevel,
                        Crit = c.Stats.Crit,
                        CritPerLevel = c.Stats.CritperLevel,
                        AttackDamage = c.Stats.AttackDamage,
                        AttackDamagePerLevel = c.Stats.AttackdamageperLevel,
                        AttackSpeed = c.Stats.AttackSpeed,
                        AttackSpeedPerLevel = c.Stats.AttackspeedperLevel
                    },
                    Passive = new PassiveDto
                    {
                        Name = c.Passive.Name,
                        Description = c.Passive.Description,
                        Image = c.Passive.Image
                    },
                    Spells = c.Spells.Select(s => new SpellDto
                    {
                        Key_board = s.Key_board,
                        Name = s.Name,
                        Description = s.Description,
                        Image = s.Image
                    }).ToList(),
                    Skins = c.Skins.Select(s => new SkinDto
                    {
                        Name = s.Name,
                        Splash = s.Splash,
                        Loading = s.Loading,
                        Model_view = s.Model_view
                    }).ToList()
                }).ToListAsync();

            var result = champions.ToDictionary(c => c.Details!.Name, c => c);

            if (!result.Any())
            {
                return NotFound($"Nenhum campeão encontrado com o nome '{championName}'.");
            }

            return Ok(result);
        }


    }
}