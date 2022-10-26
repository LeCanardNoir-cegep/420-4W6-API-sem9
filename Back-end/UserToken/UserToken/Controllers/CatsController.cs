using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserToken.Data;
using UserToken.Models;

namespace UserToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatsController : ControllerBase
    {
        private readonly UserTokenContext _context;
        private readonly UserManager<Owner> _userManager;
        IHttpContextAccessor httpContextAccessor;

        public CatsController(UserTokenContext context, UserManager<Owner> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        // GET: api/Cats
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cat>>> GetCat()
        {
            return await _context.Cat.ToListAsync();
        }

        // GET: api/Cats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> GetCat(int id)
        {
            var cat = await _context.Cat.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        // PUT: api/Cats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCat(int id, Cat cat)
        {
            if (id != cat.Id)
            {
                return BadRequest();
            }

            _context.Entry(cat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(id))
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

        // POST: api/Cats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cat>> PostCat(Cat cat)
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if( userid == null)
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur invalide." });

            cat.Owner_Id = userid;
            _context.Cat.Add(cat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCat", new { id = cat.Id }, cat);
        }

        // DELETE: api/Cats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            var cat = await _context.Cat.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            _context.Cat.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatExists(int id)
        {
            return _context.Cat.Any(e => e.Id == id);
        }
    }
}
