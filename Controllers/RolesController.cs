using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMO_4.Data;
using AMO_4.Models;
using Microsoft.AspNetCore.Authorization;

namespace AMO_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly MyWebApiContext _context;

        public RolesController(MyWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        //voir les roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.Include(b => b.User).ToListAsync();
        }

        // GET: api/Roles/5
        //voir un role
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
           

            var role = await _context.Roles.Include(b => b.User).FirstOrDefaultAsync(i => i.roleId == id);

            if (role == null)
            {
                return NotFound();
            }

            return role;

        }

        // PUT: api/Roles/5
        //modifier un role
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.roleId)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        //ajouter un role à un utilisateur
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("Add")]
        public async Task<ActionResult<Role>> PostRoleUser(Role role)
        {
            var selectRoles = _context.Roles.Include(p => p.User).SingleOrDefault(p => p.roleId == role.roleId);

       
                var existingUser = _context.Users.SingleOrDefault(p => p.userId == role.User.First().userId);
            
                selectRoles.User.Add(existingUser);
          
                 await _context.SaveChangesAsync();

            
            return CreatedAtAction("GetRole", new { id = role.roleId }, role);
        }

        //modifier un role à un utilisateur
        [HttpPost]
        [Route("Remove")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Role>> RemoveRoleUser(Role role)
        {
            var selectRoles = _context.Roles.Include(p => p.User).SingleOrDefault(p => p.roleId == role.roleId);
            var existingUser = _context.Users.SingleOrDefault(p => p.userId == role.User.First().userId);
            selectRoles.User.Remove(existingUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.roleId }, role);
        }

        //creer un nouveau role
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Role>> PostRole(Role role)

        {
         
            _context.Roles.Add(role);
          
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.roleId }, role);
         
        }




        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRoleUser(int Id)
        {
            {
                var role = _context.Roles
                    .Include(p => p.User)
                    .SingleOrDefault(p => p.roleId == Id);
           
                    var userToRemove = role.User

                    .Single(x => x.userId == Id);
                    role.User.Remove(userToRemove);

                    await _context.SaveChangesAsync();

                return NoContent();
            }


        }
        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.roleId == id);
        }
    }
}
