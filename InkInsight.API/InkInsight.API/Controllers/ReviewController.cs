using InkInsight.API.Entities;
using InkInsight.API.Persistences;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InkInsight.API.Controllers
{
    [Route("api/Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewDbContext _dbContext;

        public ReviewController(ReviewDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var reviews = _dbContext.Reviews;
            Console.WriteLine(reviews.Count);
            return Ok(reviews);
        }

        [HttpPost]
        public IActionResult Post(Review review)
        {
            _dbContext.Reviews.Add(review);
            return Created("",review);
        }
    }
}
