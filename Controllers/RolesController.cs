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
using AMO_4.Services;
using System.Net;

namespace AMO_4.Controllers
{
    /// <summary>
    /// Contrôleur des roles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly MyWebApiContext _context;
       
        public RolesController(MyWebApiContext context)
        {
             _context = context;
        }
       
        /// <returns>Liste des roles avec les utilisateurs associés</returns>
        /// <response code="200">Succès</response>
        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.Include(b => b.User).ToListAsync();
          
        }
        
        /// <param name="name">nom du rôle à récupérer</param>
        /// <returns>Retourne le rôle demandé</returns>
        /// <response code="200">Succès !</response>
        /// <response code="404">Rôle non trouvé</response>
        [HttpGet]
        [Route("name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Role>> GetRoleByName(string name)
        {
            var roleName = await _context.Roles.FirstOrDefaultAsync(i => i.name == name);
            if (roleName == null)
            {
                return NotFound();
            }
            return roleName;
        }
        
        /// <param name="id">id du rôle à récupérer</param>
        /// <returns>Retourne le rôle demandé</returns>
        /// <response code="200">Succès !</response>
        /// <response code="404">Rôle non trouvé</response>
        //GET: api/Roles/5
        //voir un role
        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.Include(b => b.User).FirstOrDefaultAsync(i => i.roleId == id);
            //var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }
      
        /// <param name="id">id du rôle à mettre à jour</param>
        /// <param name="role">Nouvelles informations du rôle</param>
        /// <returns></returns>
        /// <response code="204">Role mis à jour</response>
        /// <response code="400">Id différent du id du rôle envoyé</response>
        /// <response code="404">Le rôle n'existe pas</response>
        //PUT: api/Roles/5
        // modifier un role
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.roleId)
            {
                return BadRequest("Invalid client request");
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
                    return NotFound("le role n'est pas trouvé");
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
                var role = _context.Roles.Include(p => p.User).SingleOrDefault(p => p.roleId == Id);
                var userToRemove = role.User.Single(x => x.userId == Id);
                role.User.Remove(userToRemove);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
        
        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.roleId == id);
        }
        ////private bool RoleExists(int id)
        ////{
        ////    var roleExists = _rolesService.RoleExists(id);
        ////        return roleExists;
        ////}

    }
}
