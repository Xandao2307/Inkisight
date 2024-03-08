using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;
using InkInsight.API.Persistences;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InkInsight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly InkInsightDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookController(InkInsightDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Books.Include(b => b.Reviews));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var book = _dbContext.Books.Include(b => b.Reviews).FirstOrDefault(x => x.Id == id);
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookDTO book)
        {

            if (_dbContext.Books.Find(book.Id) != null)
                return BadRequest("This book already exist");

            var bk = _mapper.Map<Book>(book);
            _dbContext.Books.Add(bk);
            _dbContext.SaveChanges();
            return Ok(book);    
        }

  
    }
}
