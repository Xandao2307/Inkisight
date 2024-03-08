﻿using AutoMapper;
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

        public ReviewController(InkInsightDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reviews = _dbContext.Reviews.Include(r => r.Book);
            
            return Ok(reviews);
        }

        [HttpPost]
        public IActionResult Post(ReviewDTO reviewDTO)
        {
            var book = _dbContext.Books.Find(reviewDTO.BookId);
            if (book == null)
                return NotFound("Book not found.");
            
            var review = _mapper.Map<Review>(reviewDTO);
            review.Book = book;

            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Review review)
        {
            var rev = _dbContext.Reviews.SingleOrDefault(r => r.Id == id);

            if (rev == null)
            {
                return NotFound();
            }
            _dbContext.SaveChanges();
            rev = review;
            
            return NoContent();
        }

        [HttpDelete("{id}")] // Você também deve considerar usar um identificador para a exclusão
        public IActionResult Delete(Guid id)
        {
            var rev = _dbContext.Reviews.Find(id);
            if (rev == null)
            {
                return NotFound();
            }

            _dbContext.Reviews.Remove(rev);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
