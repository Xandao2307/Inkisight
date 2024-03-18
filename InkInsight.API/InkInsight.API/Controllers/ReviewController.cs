using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;
using InkInsight.API.Persistences;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InkInsight.API.Controllers
{
    [Route("api/Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly InkInsightDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(InkInsightDbContext context, IMapper mapper, ILogger<ReviewController> logger)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var reviews = _dbContext.Reviews.Include(r => r.Book).Include(r => r.User);
                return Ok(reviews);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var review = _dbContext.Reviews.Include(r => r.Book).Include(r => r.User).FirstOrDefault(x => x.Id == id);
                if (review == null)
                    return BadRequest(review);
                return Ok(review);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpPost]
        public IActionResult Post(ReviewDTO reviewDTO)
        {
            try
            {
                var book = _dbContext.Books.Find(reviewDTO.BookId);
                var user = _dbContext.Users.Find(reviewDTO.UserId);
                if (book == null || user == null)
                    return BadRequest("Invalid Params");

                var review = _mapper.Map<Review>(reviewDTO);
                review.Book = book;
                review.User = user;
                _dbContext.Reviews.Add(review);
                _dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpPut]
        public IActionResult Put(ReviewDTO review)
        {
            try
            {
                var rev = _dbContext.Reviews.SingleOrDefault(r => r.Id == review.Id);

                if (rev == null)
                    return NotFound("This review doesn't exist");

                rev.Text = review.Text;
                rev.Date = review.Date;
                rev.Rating = review.Rating;

                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpDelete("{id}")] // Você também deve considerar usar um identificador para a exclusão
        public IActionResult Delete(Guid id)
        {
            try
            {
                var rev = _dbContext.Reviews.Find(id);
                if (rev == null)
                    return NotFound("This review doesn't exist");

                _dbContext.Reviews.Remove(rev);
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Error");
            }
        }
    }
}
