using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;


namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HospitalContext _context;

        public UsersController(HospitalContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Wishes)
                .Include(u => u.Reviews)
                .Include(u => u.Roles)
                .ToListAsync();

            var mappedUsers = users.Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                Wishes = u.Wishes,
                Reviews = u.Reviews,
                Roles = u.Roles.Select(r => new Role 
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                }).ToList()
            });

            return mappedUsers.ToList(); 
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Wishes)
                .Include(u => u.Reviews)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] UserRegister model)
        {
            // Check if the username is already in use
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                return Conflict("Username already exists.");
            }

            // Check if the email is already in use
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return Conflict("Email already exists.");
            }
            
            int maxUserId = await _context.Users.MaxAsync(u => (int?)u.UserId) ?? 0;
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                Wishes = new List<Wish>(),
                Reviews = new List<Review>(),
                Roles = new List<Role>(),
                UserId = maxUserId + 1
            };
            user.Roles.Add(role);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
