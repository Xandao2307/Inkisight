using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;
using InkInsight.API.Persistences;
using InkInsight.API.Services;
using InkInsight.API.Utils;
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
        private readonly TokenService _tokenService;
        public UserController(InkInsightDbContext dbContext, IMapper mapper, TokenService tokenService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenService = tokenService;
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
            userModel.Password = HashUtils.CreateHash(userModel.Password);
            userModel.Id = Guid.NewGuid();
            _dbContext.Users.Add(userModel);
            _dbContext.SaveChanges();
            return Ok(userModel);

        }

        [HttpGet("/login")]
        public IActionResult Login(string email,string password) 
        {
            var user = _dbContext.Users.FirstOrDefault(x=> x.Email == email);
            if (user == null) 
                return NotFound("User don't exist");
            if (user.Password == HashUtils.CreateHash(password))
            {
                var token = _tokenService.GenerateToken<User>(user);
                return Ok(token);
            }
            return BadRequest();
        }
    }
}
