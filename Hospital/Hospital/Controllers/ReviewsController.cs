using AutoMapper;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;

        public ReviewsController(HospitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return _mapper.Map<List<ReviewDTO>>(reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReviewDTO>(review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDTO reviewDTO)
        {
            //if (id != reviewDTO.ReviewId)
            //{
            //    return BadRequest();
            //}

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _mapper.Map(reviewDTO, review);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO reviewDTO)
        {
            var review = _mapper.Map<Review>(reviewDTO);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.ReviewId }, reviewDTO);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
