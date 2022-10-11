using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMO_4.Data;
using AMO_4.Models;
using AMO_4.Services;

namespace AMO_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {

        private readonly MyWebApiContext _context;

        public LogsController(MyWebApiContext context)
        {
            _context = context;
        }

      
                // GET: api/Logs
                //[HttpGet]
                //public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
                //{
                    
                //    return await _context.Logs.Include(b => b.produit).Include(c => c.user).ToListAsync();
                //}
                [HttpGet]
                public async Task<List<Log>> GetDate(string dateStart, string dateEnd)
                {
                    DateTime datefilterStart;
                    DateTime datefilterEnd;
                    if (DateTime.TryParse(dateStart, out datefilterStart) && DateTime.TryParse(dateEnd, out datefilterEnd))
                    {

                        return await _context.Logs.Where(x => x.date_heure >= datefilterStart && x.date_heure < datefilterEnd).OrderByDescending(p => p.date_heure).Include(b => b.produit).Include(c => c.user).ToListAsync();
                    }
                    return await _context.Logs.Where(x => x.date_heure >= DateTime.Today && x.date_heure < DateTime.Today.AddDays(1)).Include(b => b.produit).Include(c => c.user).Take(2000).ToListAsync();

                }
                [HttpGet]
                [Route("DateNow")]
                public async Task<List<Log>> GetDateNow()
                {
                    return await _context.Logs.Where(x => x.date_heure >= DateTime.Today && x.date_heure < DateTime.Today.AddDays(1)).ToListAsync();
                }
                [HttpGet]
                [Route("Search")]
               
                public async Task<List<Log>> GetWord(string word)
                {
                     var mots = _context.Logs.Where(x => x.description.Contains(word));
                     return await mots.Include(b => b.produit).Include(c => c.user).ToListAsync();
                }


                // GET: api/Logs/5
                [HttpGet("{id}")]

                public async Task<ActionResult<Log>> GetLog(int id)
                {
                    var log = await _context.Logs.Include(b => b.produit).Include(c => c.user).SingleOrDefaultAsync(b => b.Id == id);

                     if (log == null)
                    {
                        return NotFound();
                    }

                    return log;
                   }

                // PUT: api/Logs/5
                [HttpPut("{id}")]
                public async Task<IActionResult> PutLog(int id, Log log)
                {
                    if (id != log.Id)
                    {
                        return BadRequest();
                    }

                    _context.Entry(log).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LogExists(id))
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

                 // POST: api/Logs
                  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPost]
                public async Task<ActionResult<Log>> PostLog(Log log)
                {
                    log.date_heure = DateTime.Now;
                    _context.Logs.Add(log);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetLog", new { id = log.Id }, log);
                }

                private bool LogExists(int id)
                {
                    return _context.Logs.Any(e => e.Id == id);
                }




    }
}
