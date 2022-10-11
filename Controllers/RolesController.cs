using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMO_4.Data;
using AMO_4.Models;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.Include(b => b.User).ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //        var book = context.Books
        //        .Include(p => p.Tags)
        //          .First();

        //        var tagToRemove = book.Tags
        //            .Single(x => x.TagId == "Editor's Choice");
        //        book.Tags.Remove(tagToRemove);
        //context.SaveChanges();

        //public void Update(int id,Role updatedGroup)
        //{
        //    Role role = _context.Roles
        //        .Include(g => g.User)
        //        .FirstOrDefault(g => g.roleId == updatedGroup.roleId);

        //    role.User = updatedGroup.User
        //        .Select(u => role.User.FirstOrDefault(ou => ou.userId == u.userId) ?? u)
        //        .ToList();

        //    // Do other changes on group as needed.

        //    _context.SaveChanges();
        //}
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<Role>> PostRoleUser(Role role)
        {
            var selectRoles = _context.Roles.Include(p => p.User).SingleOrDefault(p => p.roleId == role.roleId);

       
                var existingUser = _context.Users.SingleOrDefault(p => p.userId == role.User.First().userId);
            
                selectRoles.User.Add(existingUser);
          
                 await _context.SaveChangesAsync();

            
            return CreatedAtAction("GetRole", new { id = role.roleId }, role);
        }

        [HttpPost]
        [Route("Remove")]
    
        public async Task<ActionResult<Role>> RemoveRoleUser(Role role)
        {
            var selectRoles = _context.Roles.Include(p => p.User).SingleOrDefault(p => p.roleId == role.roleId);
            var existingUser = _context.Users.SingleOrDefault(p => p.userId == role.User.First().userId);
            selectRoles.User.Remove(existingUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.roleId }, role);
        }
        [HttpPost]
        
        //public async Task<ActionResult<Role>> PostRole(Role role)

        //{
        //    var existingUser = _context.Users.SingleOrDefault(t => t.userId == 1);
            
        //    role.User.Add(existingUser);
        //    _context.Roles.Add(role);
            



        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRole", new { id = role.roleId }, existingUser);
           
            

        //}
        [HttpPost]
        [Route("go")]
        public async Task<ActionResult<Role>> PostGo( Role role)
        {
            
            var existingTag1 = _context.Users.Single(t => t.userId == 1);
            var existingTag2 = _context.Users.Single(t => t.userId == 2);
            var newRole = new Role()
            {
                name = "My Book bis",
                color = "essai",
                description ="essai",
                User = new List<User>()
            };
            newRole.User.Add(existingTag1);
            newRole.User.Add(existingTag2);
            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.roleId }, newRole);



        }
        [HttpDelete("{id}")]
        //[Route("no")]
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

                    
               // }
                return NoContent();
            }


        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var role = await _context.Roles.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Roles.Remove(role);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.roleId == id);
        }
    }
}
