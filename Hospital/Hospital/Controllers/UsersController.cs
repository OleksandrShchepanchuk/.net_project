﻿using AutoMapper;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;

        public UsersController(HospitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Wishes)
                .Include(u => u.Reviews)
                .Include(u => u.Roles)
                .ToListAsync();

            return _mapper.Map<List<UserDTO>>(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
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

            return _mapper.Map<UserDTO>(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            //if (id != userDTO.UserId)
            //{
            //    return BadRequest();
            //}

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userDTO, user);

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
        
        public static bool validateEmail (string email){
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserRegister model)
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
            
            if (!validateEmail(model.Email))
            {
                return Conflict("Incorrect email");
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
            return CreatedAtAction("GetUser", new { id = user.UserId }, _mapper.Map<UserDTO>(user));
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
