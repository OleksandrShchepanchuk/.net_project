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
    public class ArticlesController : ControllerBase
    {
        private readonly HospitalContext _context;
        private readonly IMapper _mapper;

        public ArticlesController(HospitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles()
        {
            var articles = await _context.Articles.ToListAsync();
            return _mapper.Map<List<ArticleDTO>>(articles);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return _mapper.Map<ArticleDTO>(article);
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, ArticleDTO articleDTO)
        {

            //  NEED FIX

            //if (id != articleDTO.ArticleId)
            //{
            //    return BadRequest();
            //}

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _mapper.Map(articleDTO, article);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        [HttpPost]
        public async Task<ActionResult<ArticleDTO>> PostArticle(ArticleDTO articleDTO)
        {
            var article = _mapper.Map<Article>(articleDTO);
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.ArticleId }, articleDTO);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
