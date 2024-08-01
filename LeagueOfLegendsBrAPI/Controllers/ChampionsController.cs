using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeagueOfLegendsBrAPI.Data;
using LeagueOfLegendsBrAPI.Models;

namespace LeagueOfLegendsBrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        private readonly LeagueOfLegendsContext _context;

        public ChampionsController(LeagueOfLegendsContext context)
        {
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
        public async Task<ActionResult<IEnumerable<Champion>>> GetChampions()
        {
            return await _context.Champion.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Champion>> GetChampion(string id)
        {
            var champion = await _context.Champion.FindAsync(id);

            if (champion == null)
            {
                return NotFound();
            }

            return champion;
        }

        [HttpPost]
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
        }

        private bool ChampionExists(string id)
        {
            return _context.Champion.Any(e => e.Key == id);
        }
    }
}
