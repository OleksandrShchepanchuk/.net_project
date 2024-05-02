using AutoMapper;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;

        public WishesController(HospitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Wishes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishDTO>>> GetWishes()
        {
            var wishes = await _context.Wishes.ToListAsync();
            return _mapper.Map<List<WishDTO>>(wishes);
        }

        // GET: api/Wishes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishDTO>> GetWish(int id)
        {
            var wish = await _context.Wishes.FindAsync(id);

            if (wish == null)
            {
                return NotFound();
            }

            return _mapper.Map<WishDTO>(wish);
        }

        // PUT: api/Wishes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWish(int id, WishDTO wishDTO)
        {
            //if (id != wishDTO.WishId)
            //{
            //    return BadRequest();
            //}

            var wish = await _context.Wishes.FindAsync(id);
            if (wish == null)
            {
                return NotFound();
            }

            _mapper.Map(wishDTO, wish);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishExists(id))
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

        // POST: api/Wishes
        [HttpPost]
        public async Task<ActionResult<WishDTO>> PostWish(WishDTO wishDTO)
        {
            var wish = _mapper.Map<Wish>(wishDTO);
            _context.Wishes.Add(wish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWish", new { id = wish.WishId }, _mapper.Map<WishDTO>(wish));
        }

        // DELETE: api/Wishes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWish(int id)
        {
            var wish = await _context.Wishes.FindAsync(id);
            if (wish == null)
            {
                return NotFound();
            }

            _context.Wishes.Remove(wish);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishExists(int id)
        {
            return _context.Wishes.Any(e => e.WishId == id);
        }
    }
}
