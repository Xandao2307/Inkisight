using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;
using InkInsight.API.Persistences;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InkInsight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly InkInsightDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserController(InkInsightDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = _dbContext.Users.Include(u => u.Reviews);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) 
        {
            var user = _dbContext.Users.Include(u => u.Reviews).FirstOrDefault(u => u.Id == id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(UserDTO user)
        {
            if (user == null)
                return BadRequest();
            var userModel = _mapper.Map<User>(user);
            _dbContext.Users.Add(userModel);
            _dbContext.SaveChanges();
            return Ok(userModel);

        }
    }
}
