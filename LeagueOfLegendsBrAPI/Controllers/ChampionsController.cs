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
    public class ChampionsController : ControllerBase
    {
        private readonly ILogger<ChampionsController> _logger;
        private readonly LeagueOfLegendsContext _context;

        public ChampionsController(ILogger<ChampionsController> logger, LeagueOfLegendsContext context)
        {
            _logger = logger;
            _context = context;
        }

        /*         [HttpGet("test-connection")]
                public async Task<IActionResult> TestConnection()
                {
                    try
                    {
                        var champions = await _context.Champions.ToListAsync();
                        return Ok(new { Message = "Database connection successful", ChampionsCount = champions.Count });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new { Message = "Database connection failed", Error = ex.Message });
                    }
                } */
[HttpGet]
public async Task<ActionResult<IEnumerable<ChampionDto>>> GetChampions()
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

    // Log the stats
    foreach (var champion in champions)
    {
        _logger.LogInformation($"Champion: {champion.Name}, Stats: {JsonConvert.SerializeObject(champion.Stats)}");
    }

    return champions;
}
        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Champion>>> GetChampionsFull()
        {
            return await _context.Champion
                .Include(c => c.Info)
                .Include(c => c.Stats)
                .Include(c => c.Passive)
                .Include(c => c.Spells)
                .Include(c => c.Skins)
                .ToListAsync();
        }

        [HttpGet("byName/{name}")]
        public async Task<ActionResult<Champion>> GetChampionByName(string name)
        {
            var champion = await _context.Champion
                .Include(c => c.Info)
                .Include(c => c.Stats)
                .Include(c => c.Passive)
                .Include(c => c.Spells)
                .Include(c => c.Skins)
                .FirstOrDefaultAsync(c => c.Name == name);

            if (champion == null)
            {
                return NotFound();
            }

            return champion;
        }

        [HttpGet("full/{name}")]
        public async Task<ActionResult<Champion>> GetChampionFullByName(string name)
        {
            var champion = await _context.Champion
                .Include(c => c.Info)
                .Include(c => c.Stats)
                .Include(c => c.Passive)
                .Include(c => c.Spells)
                .Include(c => c.Skins)
                .FirstOrDefaultAsync(c => c.Name == name);

            if (champion == null)
            {
                return NotFound();
            }

            return champion;
        }


        [HttpGet("byId/{id}")]
        public async Task<ActionResult<Champion>> GetChampionById(string id)
        {
            var champion = await _context.Champion
                .Include(c => c.Info)
                .Include(c => c.Stats)
                .Include(c => c.Passive)
                .Include(c => c.Spells)
                .Include(c => c.Skins)
                .FirstOrDefaultAsync(c => c.Key == id);

            if (champion == null)
            {
                return NotFound();
            }

            return champion;
        }


        /*         [HttpPost]
                public async Task<ActionResult<Champion>> PostChampion(Champion champion)
                {
                    _context.Champion.Add(champion);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetChampion), new { id = champion.Key }, champion);
                }

                [HttpPut("{id}")]
                public async Task<IActionResult> PutChampion(string id, Champion champion)
                {
                    if (id != champion.Key)
                    {
                        return BadRequest();
                    }

                    _context.Entry(champion).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ChampionExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return NoContent();
                }

                [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteChampion(string id)
                {
                    var champion = await _context.Champion.FindAsync(id);
                    if (champion == null)
                    {
                        return NotFound();
                    }

                    _context.Champion.Remove(champion);
                    await _context.SaveChangesAsync();

                    return NoContent();
                } */

        private bool ChampionExists(string id)
        {
            return _context.Champion.Any(e => e.Key == id);
        }
    }
}
