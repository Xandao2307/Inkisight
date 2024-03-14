using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;
using InkInsight.API.Persistences;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InkInsight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly InkInsightDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;

        public BookController(InkInsightDbContext dbContext, IMapper mapper, ILogger<BookController> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_dbContext.Books.Include(b => b.Reviews));
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error endpoint GET from BooksController, error message: ");

                return StatusCode(500, "Internal Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var book = _dbContext.Books.Include(b => b.Reviews).FirstOrDefault(x => x.Id == id);
                if (book == null)
                    return NotFound(book);
                return Ok(book);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error endpoint GETBYID from BooksController, error message: ");
                return StatusCode(500, "Internal Error");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] BookDTO book)
        {
            try
            {
                if (_dbContext.Books.Find(book.Id) != null)
                    return BadRequest("This book already exist");

                var bk = _mapper.Map<Book>(book);
                _dbContext.Books.Add(bk);
                _dbContext.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error endpoint POST from BooksController, error message: ");

                return StatusCode(500, "Internal Error");
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] BookDTO book)
        {
            try
            {
                var bk = _dbContext.Books.Find(book.Id);

                if (bk == null)
                    return NotFound("This book doesn't exist");

                bk.Description = book.Description;
                bk.Name = book.Name;
                bk.Author = book.Author;

                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error endpoint PUT from BooksController, error message: ");

                return StatusCode(500, "Internal Error");
            }

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var bk = _dbContext.Books.Find(id);
                if (bk == null)
                    return NotFound("This book doesn't exist");

                _dbContext.Books.Remove(bk);
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error endpoint DELETE from BooksController, error message: ");

                return StatusCode(500, "Internal Error");
            }
        }
    }
}
